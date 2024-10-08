using ETicaretDemo.Dto;
using ETicaretDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ETicaretDemo.Controllers
{
    public class HomeController : Controller
    {
        Context con = new Context();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (login.Email == "denizsinan191@gmail.com" && login.Password == "1")
            {
                return RedirectToAction("Home", "Home");//Homecontrollerdan Home sayfasına yönlendirsin.
            }
            TempData["Hata"] = "Şifre ya da kullanıcı adı hatalı";
            return RedirectToAction("Index", "Home");
            //Değerler uyuşmuyorsa Homecontrollerdan Index sayfasına yönelndiricek.
        }


        [HttpGet]
        public IActionResult Home()
        {
            //    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Urunler", connection);
            //    DataTable dataTable = new DataTable();
            //    dataTable.Clear();
            //    adapter.Fill(dataTable);

            //    List<Urun> urunlerim = new List<Urun>();
            //    for (int i = 0; i < dataTable.Rows.Count; i++)
            //    {
            //        Urun urunler = new Urun();
            //        urunler.Id = Convert.ToInt16(dataTable.Rows[i]["id"].ToString());
            //        urunler.UrunAdi = dataTable.Rows[i]["UrunAdi"].ToString();
            //        urunler.Resim = dataTable.Rows[i]["Resim"].ToString();
            //        urunler.BirimFiyati = Convert.ToDecimal(dataTable.Rows[i]["BirimFiyati"].ToString());

            //        urunlerim.Add(urunler); 
            //    }

            List<Urun> urunlerim = new List<Urun>();
            urunlerim = con.Urunler.ToList();

            List<Sepet> sepetim = new List<Sepet>();
            sepetim = con.Sepetim.ToList();

            HomeDto list = new HomeDto();
            list.Urunlerim = urunlerim;
            list.Sepetim = sepetim;




            return View(list);
        }

        [HttpPost]
        public IActionResult SepeteEkle(int id, int adet)
        {
            Urun urun = con.Urunler.Where(p => p.id == id).FirstOrDefault();

            Sepet sepet = new Sepet();
            sepet.UrunAdi = urun.UrunAdi;
            sepet.Adet = adet;
            sepet.BirimFiyatı = urun.BirimFiyati;
            sepet.Toplam = sepet.Adet * sepet.BirimFiyatı;

            con.Sepetim.Add(sepet);
            con.SaveChanges();

            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult SepettekiUrunuSil(int id)
        {
            Sepet sepet = con.Sepetim.Where(p =>p.id == id).FirstOrDefault();
            con.Sepetim.Remove(sepet);
            con.SaveChanges();
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult OdemeYap()
        {
            var result = con.Sepetim.ToList();
            foreach (var item in result)
            {
                con.Sepetim.Remove(item);
                con.SaveChanges();
            }
            TempData["odeme"] = "Ödeme Başarıyla Yapıldı";
            return RedirectToAction("Home", "Home");
        }
    }
}