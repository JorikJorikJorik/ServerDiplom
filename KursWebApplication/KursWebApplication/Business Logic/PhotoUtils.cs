using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class PhotoUtils
    {
        private string[] arrayPhotoUrl = new string[]
        {   "http://www.hdiphonewallpapers.us/phone-wallpapers/phone/12963B35351M0-5TW.jpg",
            "http://www.hdiphonewallpapers.us/phone-wallpapers/phone/1296363c5J110-23224.jpg",
            "http://www.floriculture.ru/forum/images/avatars/65949596497358305462f.jpg",
            "http://naturewallpaperfree.com/sky/nature-wallpaper-128x128-62-ed227705.jpg",
            "http://4.bp.blogspot.com/-mGdGiU3DU80/UciQDH5uURI/AAAAAAAAAUU/AQQtlMdiMfM/s1600/Nature+Wallpapers+(34).jpg",
            "http://2.bp.blogspot.com/-7W6e9GRPp2M/TcRZTNTjgkI/AAAAAAAAGIM/jN3c1862QiM/s1600/Nature%2BWallpapers%2B%25289%2529.jpg",
            "http://naturewallpaperfree.com/forest-autumn/nature-wallpaper-128x128-4198-29ff1f48.jpg",
            "http://mnogoava.ru/wp-content/uploads/2010/02/avatar21-6.jpg",
            "http://naturewallpaperfree.com/sky/nature-wallpaper-128x128-4132-dc72d705.jpg",
            "http://naturewallpaperfree.com/waterfalls/nature-wallpaper-128x128-4193-d11fe4bf.jpg",
            "http://naturewallpaperfree.com/flowers/nature-wallpaper-128x128-4196-37362ffb.jpg",
            "http://www.hdiphonewallpapers.us/phone-wallpapers/phone/1296364I194c0-5Y24.jpg",
            "http://naturewallpaperfree.com/sky/nature-wallpaper-128x128-52-74c01104.jpg",
            "http://naturewallpaperfree.com/insects/nature-wallpaper-128x128-4194-33c3ffc6.jpg",
            "http://naturewallpaperfree.com/winter/nature-wallpaper-128x128-4184-f24d2006.jpg",
            "http://4.bp.blogspot.com/-iL2-WB3wg9g/TcRZbR_oQEI/AAAAAAAAGIc/ll1DF20CbBQ/s1600/Nature%2BWallpapers%2B%252811%2529.jpg",
            "http://naturewallpaperfree.com/sky/nature-wallpaper-128x128-59-8c20eaf3.jpg",
            "http://www.beach-backgrounds.com/wallpapers/awesome-noon-on-tropical-beach-wallpaper-128x128-252.jpg",
            "http://www2.hiren.info/download/mobile-phone/wallpapers/phone-wallpaper-hm.jpg",
            "http://1.bp.blogspot.com/-VPSspxkgGMQ/TgQ3XQp9PQI/AAAAAAAAAcQ/XzYhMGQQC48/s1600/ekalavya+natural+wallpaper+14.jpg",
            "http://f1.pepst.com/c/0719DC/384151/ssc3/home/042/lalu145/nature.jpg_480_480_0_64000_0_1_0.jpg"
        };

        public string generateRundomUrlPhoto()
        {
            Random random = new Random();
            return arrayPhotoUrl[random.Next(0, 20)];
        }
    }
}