using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using VMS.CA.Scripting;
using VMS.DV.PD.Scripting;
using System.Drawing;

namespace portalanalysis1 {

  static class Program {

    [STAThread]
    static void Main(string[] args) {
      try {
        using (Application app = Application.CreateApplication()) {
          Execute(app);
        }
      }
      catch (Exception e) {
        Console.Error.WriteLine(e.ToString());
      }
    }

    static void Execute(Application app) {
        // TODO: add here your code
            //Console.WriteLine("Enter patient Id");
            //string patientId = Console.ReadLine();
            string patientId = "RA QA";
            var patient = app.OpenPatientById(patientId);
            if (patient == null)
                return;

            Course c1 = patient.Courses.FirstOrDefault();
            Console.WriteLine("Course: " + c1.Name);


            PlanSetup plan1 = c1.PlanSetups.FirstOrDefault(plan => plan.Id.Contains("PF Stat M"));
            Console.WriteLine("Plan: "+ plan1.Id);

            Beam beam1 = plan1.Beams.FirstOrDefault(beam=>beam.Id.Contains("PF Stat 0"));
            Console.WriteLine(beam1.Id);
            Console.WriteLine("Number of control points: " + beam1.ControlPoints.Count());

            ProjectionImage image1 = beam1.FieldImages.FirstOrDefault();

            Console.WriteLine("Projection image: " + image1.Id);
            Console.WriteLine("Image frames: " + image1.Frames.Count());
            

            Frame frame1 = image1.Frames[0];
            ushort[,] image = new ushort[frame1.XSize, frame1.YSize];
            frame1.GetVoxels(0, image);
            DrawImage(image, frame1.XSize, frame1.YSize);

            
            




            Console.ReadKey();
    }

        public static void DrawImage(ushort[,] image1, int xSize, int ySize)
        {

            var bitmap = new Bitmap(xSize, ySize);

            for(int x = 0; x < bitmap.Width; x++)
            {
                for(int y = 0; y < bitmap.Height; y++)
                {
                    //Console.WriteLine(image1[x,y]);
                    ushort pixel = (ushort)(image1[x, y]/200);
                    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(pixel, pixel, pixel));
                    
                }
            }

            bitmap.Save("D:\\image1.bmp");

        }

  }

}
