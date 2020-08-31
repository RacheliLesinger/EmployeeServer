using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using DTOMapper;
using System.Dynamic;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.IO;

namespace Services
{
    public class AzureFaceService
    {
        // Used for the Identify faces.
        static string SUBSCRIPTION_KEY = "2f89669e6d65441683f98868f8f06e98"; // Environment.GetEnvironmentVariable("FACE_SUBSCRIPTION_KEY");
        static string ENDPOINT = "https://westeurope.api.cognitive.microsoft.com/"; // Environment.GetEnvironmentVariable("FACE_ENDPOINT");
        static string ENDPOINT1 = "https://serviceface.cognitiveservices.azure.com/";
        Guid AZURE_SUBSCRIPTION_ID = new Guid("505c8ed9-d0e3-43a8-a130-f7ce83370838");


        private static Stream GetStreamFromUrl(string url)
        {
            try
            {
                //The filename, directory name, or volume label syntax is incorrect. : 'C:\Users\Admin\Desktop\פרויקט גמר\new\EmployeeServer\EmployeeServer\https:\localhost:44370\Images\racheliImage1.jpg'
                var folder = Path.Combine(Directory.GetCurrentDirectory());
                var path = url.Substring(url.IndexOf("44370")+ 5);
                path = path.Replace("/", "\\");
                var p =folder+path;
                FileStream stream = new FileStream(p, FileMode.Open);
                return stream;
            }
            catch (Exception ex)
            {

                throw;
            }
           
            //byte[] imageData = null;

            //using (var wc = new System.Net.WebClient())
            //    imageData = wc.DownloadData(url);

            //return new MemoryStream(imageData);
          
        }

        public static async Task<Guid> AddPersonAndFaceToPersonGroup(string employeeId, string path, int employeeNumber)
        {
            try
            {

            
            // Authenticate.
            IFaceClient client = Authenticate(ENDPOINT, SUBSCRIPTION_KEY);
                string personGroupId = "mypersongroup";// "24563dd8-f00b-4220-82a4-55a6ac9676c2";
            Stream stream = GetStreamFromUrl(path);

            // create person group person.
            // Limit TPS
            await Task.Delay(250);
            Person person = await client.PersonGroupPerson.CreateAsync(personGroupId: personGroupId, name: employeeId);

            // Add face to the person group person.
            PersistedFace face = await client.PersonGroupPerson.AddFaceFromStreamAsync(personGroupId, person.PersonId,
                        stream,employeeNumber.ToString());
            
            // Start to train the person group.
            await client.PersonGroup.TrainAsync(personGroupId);
            // Wait until the training is completed.
            while (true)
            {
                await Task.Delay(1000);
                var trainingStatus = await client.PersonGroup.GetTrainingStatusAsync(personGroupId);
                //Console.WriteLine($"Training status: {trainingStatus.Status}.");
                if (trainingStatus.Status == TrainingStatusType.Succeeded) { break; }
            }

            return face.PersistedFaceId;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }


        /*
		 * IDENTIFY FACES
		 * To identify faces, you need to create and define a person group.
		 * The Identify operation takes one or several face IDs from DetectedFace or PersistedFace and a PersonGroup and returns 
		 * a list of Person objects that each face might belong to. Returned Person objects are wrapped as Candidate objects, 
		 * which have a prediction confidence value.
		 */
        public static async Task<List<string>> IdentifyInPersonGroup( string url)
        {
            try
            {
            IFaceClient client = Authenticate(ENDPOINT, SUBSCRIPTION_KEY);
            IFaceClient faceClient = Authenticate(ENDPOINT1, SUBSCRIPTION_KEY);
            string personGroupId = "24563dd8-f00b-4220-82a4-55a6ac9676c2";

            List<String> res = new List<string>();
            List<Guid?> sourceFaceIds = new List<Guid?>();
            // Detect faces from source image url.
            List<DetectedFace> detectedFaces = await DetectFaceRecognize(client, url);

            // Add detected faceId to sourceFaceIds.
            foreach (var detectedFace in detectedFaces) { sourceFaceIds.Add(detectedFace.FaceId.Value); }

            // Identify the faces in a person group. 
            var identifyResults = await faceClient.Face.IdentifyAsync(sourceFaceIds, personGroupId);

            foreach (var identifyResult in identifyResults)
            {
                Person person = await faceClient.PersonGroupPerson.GetAsync(personGroupId, identifyResult.Candidates[0].PersonId);
                res.Add(person.Name + " " + person.PersonId);
            }
            return res;

            }
            catch (APIErrorException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Detect faces from image url for recognition purpose. This is a helper method for other functions in this quickstart.
        // Parameter `returnFaceId` of `DetectWithUrlAsync` must be set to `true` (by default) for recognition purpose.
        // The field `faceId` in returned `DetectedFace`s will be used in Face - Find Similar, Face - Verify. and Face - Identify.
        // It will expire 24 hours after the detection call.
        private static async Task<List<DetectedFace>> DetectFaceRecognize(IFaceClient faceClient, string url)
        {
            Stream stream = GetStreamFromUrl(url);
            // Detect faces from image URL. use the recognition model 3.
            IList<DetectedFace> detectedFaces = await faceClient.Face.DetectWithStreamAsync(stream, true, true, recognitionModel: "recognition_03", returnRecognitionModel: true);
            return detectedFaces.ToList();
        }



        public static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }
    }
}
