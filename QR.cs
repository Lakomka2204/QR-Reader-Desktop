using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Multi.QrCode;
using ZXing.QrCode;

namespace QR_Reader_Desktop
{
    internal static class QR
    {
        public static int PPU => Settings.PPU;
        public static int IconSize => Settings.IconSize;
        public static int BorderRadius => Settings.BorderRadius;
        public static Color LightColor => Color.FromArgb(Settings.LightColor);
        public static Color DarkColor => Color.FromArgb(Settings.DarkColor);
        public static Color IconBorderColor => Color.FromArgb(Settings.IconBorderColor);
        public static bool DrawQuietZones => Settings.DrawQuietZones;
        public static Bitmap Icon
        {
            get
            {
                if (!File.Exists(Settings.IconPath)) return null;
                try
                {
                    var img = (Bitmap)Image.FromFile(Settings.IconPath);
                    return img;
                }
                catch (OutOfMemoryException)
                {
                    return null;
                }
            }
        }

        internal static class Create
        {
            static Bitmap GenerateQR(QRCode q)
                => q.GetGraphic(PPU, DarkColor, LightColor, Icon, IconSize,BorderRadius,DrawQuietZones,IconBorderColor);
            internal static Bitmap FromText(string text)
            {
                using (QRCodeGenerator gen = new QRCodeGenerator())
                using (QRCodeData data = gen.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qr = new QRCode(data))
                    return GenerateQR(qr);
            }

            static Bitmap FromBase(PayloadGenerator.Payload payload)
            {
                using (QRCodeGenerator gen = new QRCodeGenerator())
                using (QRCodeData data = gen.CreateQrCode(payload))
                using (QRCode qr = new QRCode(data))
                    return GenerateQR(qr);
            }

            internal static Bitmap FromUrl(string url)
                => FromBase(new PayloadGenerator.Url(url));

            internal static Bitmap FromPhone(string tel)
                => FromBase(new PayloadGenerator.PhoneNumber(tel));

            internal static Bitmap FromWifi(string ssid, string pwd, bool hidden, PayloadGenerator.WiFi.Authentication method)
                => FromBase(new PayloadGenerator.WiFi(ssid, pwd, method, hidden));

            internal static Bitmap FromSMS(string tel)
                => FromBase(new PayloadGenerator.SMS(tel));

            internal static Bitmap FromCalendarEvent(string subj, string desc, string location, DateTime start, DateTime end, bool allday)
                => FromBase(new PayloadGenerator.CalendarEvent(subj, desc, location, start, end, allday));

            internal static Bitmap FromBookmark(string title, string url)
                => FromBase(new PayloadGenerator.Bookmark(url, title));

            internal static Bitmap FromMail(string to, string subj, string msg)
                => FromBase(new PayloadGenerator.Mail(to, subj, msg));
        }
        internal static class Scan
        {
            static BinaryBitmap GetZBitmap(Bitmap origin)
                => new BinaryBitmap(new HybridBinarizer(new BitmapLuminanceSource(origin)));

            internal static Result FromImageFast(Bitmap image)
            {
                var source = new BitmapLuminanceSource(image);
                var bitmap = new BinaryBitmap(new HybridBinarizer(source));
                var result = new MultiFormatReader().decode(bitmap);
                return result;
            }
            internal static string FromImage(Bitmap image)
            {
                var res = new QRCodeReader().decode(GetZBitmap(image));
                return res?.Text ?? "No result";
            }
            internal static Result[] FromImageMultiple(Bitmap image)
            {
                return new QRCodeMultiReader().decodeMultiple(GetZBitmap(image));
            }
        }
    }
}
