using Dicom;
using Dicom.Imaging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Convert
{
    public class Class1
    {
        public static void ProcessImageFile(DicomDataset dataset, string filePath)
        {
            ImageManager.SetImplementation(ImageSharpImageManager.Instance);
            var dicomImage = new Dicom.Imaging.DicomImage(dataset);
            using var IImage = dicomImage.RenderImage();
            var sharpImage = IImage.AsSharpImage();

            using var memoryStream = new MemoryStream();
            sharpImage.SaveAsBmp(memoryStream);

            var bitmap = new Bitmap(memoryStream);
            bitmap = GetRgbImage(bitmap);

            var freeBitmap = (FreeImageAPI.FreeImageBitmap)bitmap;
            freeBitmap.Save($"{filePath}.jpg", FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG, (FreeImageAPI.FREE_IMAGE_SAVE_FLAGS)90);

            sharpImage.Dispose();
            bitmap.Dispose();
            freeBitmap.Dispose();
            if (dataset.Contains(DicomTag.PixelData))
            {
                dataset.Remove(DicomTag.PixelData);
            }
            StoreToJson(dataset, filePath);


        }
        private static void StoreToJson(DicomDataset dataset, string filePath)
        {
            var jsonDataset = JsonConvert.SerializeObject(dataset, new Dicom.Serialization.JsonDicomConverter());
            File.WriteAllText($"{filePath}.json", jsonDataset);
        }
        private static Bitmap GetRgbImage(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
            {
                Bitmap old = bitmap;
                using (old)
                {
                    bitmap = new Bitmap(old.Width, old.Height, PixelFormat.Format24bppRgb);
                    using Graphics g = Graphics.FromImage(bitmap);
                    g.DrawImage(old, 0, 0, old.Width, old.Height);
                }
            }
            return bitmap;
        }
    }
}
