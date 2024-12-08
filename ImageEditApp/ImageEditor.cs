using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageEditApp
{
    class ImageEditor
    {
        static int i = 0;

        static public void ImageLoad(string FolderDirectory, string LogoPathDirectory, double Percentage, string position)
        {
            string[] supportedFormats = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            foreach (string filePath in Directory.GetFiles(FolderDirectory))
            {
                string extension = Path.GetExtension(filePath).ToLower();

                if (Array.Exists(supportedFormats, format => format == extension))
                {
                    try
                    {
                        ImageEdit(filePath, LogoPathDirectory, Percentage, position);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обработке файла '{filePath}': {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            MessageBox.Show("Обработка завершена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        static public void ImageEdit(string ImagePath, string LogoPathDirectory, double Percentage, string position)
        {
            using (Bitmap mainImage = ConvertToNonIndexedImage(new Bitmap(ImagePath)))
            using (Bitmap logo = ConvertToNonIndexedImage(new Bitmap(LogoPathDirectory)))
            {
                // Рассчитать новый размер логотипа
                int newLogoWidth = (int)(mainImage.Width * Percentage / 100);
                int newLogoHeight = (int)((double)logo.Height / logo.Width * newLogoWidth);
                Bitmap resizedLogo = new Bitmap(logo, newLogoWidth, newLogoHeight);

                using (Graphics graphics = Graphics.FromImage(mainImage))
                {
                    // Рассчитать позицию логотипа
                    int x = 0, y = 0;
                    if (position == "Сверху-Слева")
                    {
                        x = 0;
                        y = 0;

                    }
                    else if (position == "Сверху-Справа")
                    {
                        x = mainImage.Width - resizedLogo.Width;
                        y = 0;

                    }
                    else if (position == "Снизу-Слева")
                    {
                        x = 0;
                        y = mainImage.Height - resizedLogo.Height;

                    }
                    else if (position == "Снизу-Справа")
                    {
                        x = mainImage.Width - resizedLogo.Width;
                        y = mainImage.Height - resizedLogo.Height;

                    }
                    else if (position == "Центр")
                    {
                        x = (mainImage.Width - resizedLogo.Width) / 2;
                        y = (mainImage.Height - resizedLogo.Height) / 2;

                    }
                    // Наложить логотип
                    graphics.DrawImage(resizedLogo, x, y, resizedLogo.Width, resizedLogo.Height);
                }
                string SavePath = Path.GetDirectoryName(ImagePath);
                // Сохранить итоговое изображение
                mainImage.Save(SavePath + @"\image" + $"{i}" + Path.GetExtension(ImagePath).ToLower());
            }
        }

        static private Bitmap ConvertToNonIndexedImage(Bitmap img)
        {
            if (img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format1bppIndexed || img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format4bppIndexed || img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                // Создаем новое изображение в формате PixelFormat.Format32bppArgb
                Bitmap newImage = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                // Копируем содержимое оригинального изображения в новый формат
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.DrawImage(img, 0, 0, img.Width, img.Height);
                }

                return newImage;
            }

            return img;
        }
    }
}
