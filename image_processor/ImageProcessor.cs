using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

///<summary>Collection of methods for image manipulation</summary>
public class ImageProcessor
{
    ///<summary>inverts the colorscale of images</summary>
    public static void Inverse(string[] filenames)
    {
        Parallel.ForEach (filenames, file => {
            CreateInverse(file);
        });
    }

    private static void CreateInverse(string file)
    {
        Bitmap bmp = new Bitmap(file);
        BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

        int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
        int byteCount = bitmapData.Stride * bmp.Height;
        byte[] pixels = new byte[byteCount];
        IntPtr ptrFirstPixel = bitmapData.Scan0;
        Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
        int heightInPixels = bitmapData.Height;
        int widthInBytes = bitmapData.Width * bytesPerPixel;

        for (int y = 0; y < heightInPixels; y++)
        {
            int currentLine = y * bitmapData.Stride;
            for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
            {
                int oldBlue = pixels[currentLine + x];
                int oldGreen = pixels[currentLine + x + 1];
                int oldRed = pixels[currentLine + x + 2];

                // calculate new pixel value
                pixels[currentLine + x] = (byte)(255 - oldBlue);
                pixels[currentLine + x + 1] = (byte)(255 - oldGreen);
                pixels[currentLine + x + 2] = (byte)(255 - oldRed);
            }
        }
 
        // copy modified bytes back
        Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
        bmp.UnlockBits(bitmapData);

        //create new file name
        string name = string.Format("{0}_inverse{1}",
                                    Path.GetFileNameWithoutExtension(file),
                                    Path.GetExtension(file));
        //save new image
        bmp.Save(name);
    }

    ///<summary>Recreates an image in grayscale</summary>
    public static void Grayscale(string[] filenames)
    {
        Parallel.ForEach (filenames, file => {
            CreateGrayscale(file);
        });
    }

    private static void CreateGrayscale(string file)
    {
        Bitmap bmp = new Bitmap(file);
        BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

        int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
        int byteCount = bitmapData.Stride * bmp.Height;
        byte[] pixels = new byte[byteCount];
        IntPtr ptrFirstPixel = bitmapData.Scan0;
        Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
        int heightInPixels = bitmapData.Height;
        int widthInBytes = bitmapData.Width * bytesPerPixel;

        for (int y = 0; y < heightInPixels; y++)
        {
            int currentLine = y * bitmapData.Stride;
            for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
            {
                int oldBlue = pixels[currentLine + x];
                int oldGreen = pixels[currentLine + x + 1];
                int oldRed = pixels[currentLine + x + 2];

                // calculate new pixel value
                int avg = (oldBlue + oldGreen + oldRed) / 3;
                pixels[currentLine + x] = (byte)avg;
                pixels[currentLine + x + 1] = (byte)avg;
                pixels[currentLine + x + 2] = (byte)avg;
            }
        }
 
        // copy modified bytes back
        Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
        bmp.UnlockBits(bitmapData);

        //create new file name
        string name = string.Format("{0}_grayscale{1}",
                                    Path.GetFileNameWithoutExtension(file),
                                    Path.GetExtension(file));
        //save new image
        bmp.Save(name);
    }

    ///<summary>Reproduces image with only black and white pixels based on set threshold</summary>
    public static void BlackWhite(string[] filenames, double threshold)
    {
        Parallel.ForEach (filenames, file => {
            CreateBlackWhite(file, threshold);
        });
    }
    
    private static void CreateBlackWhite(string file, double threshold)
    {
        Bitmap bmp = new Bitmap(file);
        BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

        int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
        int byteCount = bitmapData.Stride * bmp.Height;
        byte[] pixels = new byte[byteCount];
        IntPtr ptrFirstPixel = bitmapData.Scan0;
        Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
        int heightInPixels = bitmapData.Height;
        int widthInBytes = bitmapData.Width * bytesPerPixel;

        for (int y = 0; y < heightInPixels; y++)
        {
            int currentLine = y * bitmapData.Stride;
            for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
            {
                int oldBlue = pixels[currentLine + x];
                int oldGreen = pixels[currentLine + x + 1];
                int oldRed = pixels[currentLine + x + 2];

                // calculate new pixel value
                int avg = (oldBlue + oldGreen + oldRed) / 3;
                if ((double)avg > threshold)
                    avg = 255;
                else
                    avg = 0;
                pixels[currentLine + x] = (byte)avg;
                pixels[currentLine + x + 1] = (byte)avg;
                pixels[currentLine + x + 2] = (byte)avg;
            }
        }
 
        // copy modified bytes back
        Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
        bmp.UnlockBits(bitmapData);

        //create new file name
        string name = string.Format("{0}_bw{1}",
                                    Path.GetFileNameWithoutExtension(file),
                                    Path.GetExtension(file));
        //save new image
        bmp.Save(name);
    }


    ///<summary>Creates a thumbnail of an image at the specified height</summary>
    public static void Thumbnail(string[] filenames, int height)
    {
        Parallel.ForEach (filenames, file => {
            CreateThumbnail(file, height);
        });
    }

    private static void CreateThumbnail(string file, int newHeight)
    {
        //read image
        Bitmap bmp = new Bitmap(file);

        //get image dimensions
        int width = bmp.Width;
        int height = bmp.Height;

        //determine new width
        int newWidth = (width * newHeight) / height;

        //create new image
        Image thumb = bmp.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

        //create new file name
        string name = string.Format("{0}_th{1}",
                                    Path.GetFileNameWithoutExtension(file),
                                    Path.GetExtension(file));
        //save new image
        thumb.Save(name);
    }
}