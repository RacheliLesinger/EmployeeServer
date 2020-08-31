using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // AzureFaceService.AddPersonAndFaceToPersonGroup("456", @"C:\Users\Admin\Desktop\racheliImage.jpg", 222);
               var res = AzureFaceService.IdentifyInPersonGroup(@"C:\Users\Admin\Desktop\racheliImage.jpg");
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                throw;
            }
         }
    }
}
