using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using odevDagitimPortali.Models;
using odevDagitimPortali.ViewModel;

namespace odevDagitimPortali.Controllers
{
    public class ServisController : ApiController
    {
        odevPortalEntities1 db = new odevPortalEntities1();
        SonucModel sonuc = new SonucModel();

        #region Uye
        [HttpGet]
        [Route("api/uyelistele")]
        public List<UyeModel> UyeListele()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                kullaniciAdi = x.kullaniciAdi,
                adSoyad = x.adSoyad,
                email = x.email,
                parola = x.parola,
                uyeTipi = x.uyeTipi

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                kullaniciAdi = x.kullaniciAdi,
                adSoyad = x.adSoyad,
                email = x.email,
                parola = x.parola,
                uyeTipi = x.uyeTipi

            }).FirstOrDefault();
            return kayit;
        }
        [HttpGet]
        [Route("api/uyebyuyetipi/{uyeTipi}")]
        public List<UyeModel> UyeByUyeTipi(int uyeTipi)
        {
            List<UyeModel> liste = db.Uye.Where(s => s.uyeTipi == uyeTipi).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                kullaniciAdi = x.kullaniciAdi,
                adSoyad = x.adSoyad,
                email = x.email,
                parola = x.parola,
                uyeTipi = x.uyeTipi

            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.kullaniciAdi == model.kullaniciAdi && s.email == model.email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Kayıtlıdır";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.kullaniciAdi = model.kullaniciAdi;
            yeni.adSoyad = model.adSoyad;
            yeni.email = model.email;
            yeni.parola = model.parola;
            yeni.uyeTipi = model.uyeTipi;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Uye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Bulunamadı";
                return sonuc;
            }
            kayit.kullaniciAdi = model.kullaniciAdi;
            kayit.adSoyad = model.adSoyad;
            kayit.email = model.email;
            kayit.parola = model.parola;
            kayit.uyeTipi = model.uyeTipi;
            sonuc.islem = true;
            sonuc.mesaj = "Uye Güncellendi";
            db.SaveChanges();

            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]

        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Bulunamadı";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitUyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu üyeye kayıtlı ödev olduğu için silinemez";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Uye Silindi";
            return sonuc;
        }
        #endregion

        #region Kategori
        [HttpGet]
        [Route("api/kategorilistele")]
        public List<KategoriModel> KategoriListele()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAdi = x.kategoriAdi,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{kategoriId}")]
        public KategoriModel KategoriById(int kategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAdi = x.kategoriAdi
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriAdi == model.kategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.kategoriAdi = model.kategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Kategori Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı";
                return sonuc;
            }
            kayit.kategoriAdi = model.kategoriAdi;
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Güncellendi";
            db.SaveChanges();
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]

        public SonucModel KategoriSil(int kategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı";
                return sonuc;
            }
            if (db.Ders.Count(s => s.dersKategoriId == kategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kategorie kayıtlı Ders olduğu için silinemez";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region Ders
        [HttpGet]
        [Route("api/derslistele")]
        public List<DersModel> DersListele()
        {
            List<DersModel> liste = db.Ders.Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKodu = x.dersKodu,
                dersKredi = x.dersKredi,
                dersKategoriId = x.dersKategoriId
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.kategoriAdi = KategoriById(kayit.dersKategoriId);
            }
            return liste;
        }

        [HttpGet]
        [Route("api/dersbyid/{dersId}")]
        public DersModel DersById(int dersId)
        {
            DersModel kayit = db.Ders.Where(s => s.dersId == dersId).Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKodu = x.dersKodu,
                dersKredi = x.dersKredi,
                dersKategoriId = x.dersKategoriId,
                kategoriAdi = KategoriById(x.dersKategoriId)

            }).FirstOrDefault();
            return kayit;
        }
        [HttpGet]
        [Route("api/dersbykategoriid/{kategoriId}")]
        public List<DersModel> DersByKategoriId(int kategoriId)
        {
            List<DersModel> liste = db.Ders.Where(s => s.dersKategoriId == kategoriId).Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKodu = x.dersKodu,
                dersKredi = x.dersKredi,
                dersKategoriId = x.dersKategoriId
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.kategoriAdi = KategoriById(kayit.dersKategoriId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/dersekle")]
        public SonucModel DersEkle(DersModel model)
        {
            if (db.Ders.Count(s => s.dersAdi == model.dersAdi && s.dersKodu == model.dersKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Kayıtlıdır";
                return sonuc;
            }
            Ders yeni = new Ders();
            yeni.dersAdi = model.dersAdi;
            yeni.dersKodu = model.dersKodu;
            yeni.dersKredi = model.dersKredi;
            yeni.dersKategoriId = model.dersKategoriId;
            db.Ders.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Ders Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/dersduzenle")]
        public SonucModel DersDuzenle(DersModel model)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == model.dersId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı";
                return sonuc;
            }
            kayit.dersAdi = model.dersAdi;
            kayit.dersKodu = model.dersKodu;
            kayit.dersKredi = model.dersKredi;
            kayit.dersKategoriId = model.dersKategoriId;
            sonuc.islem = true;
            sonuc.mesaj = "Ders Güncellendi";
            db.SaveChanges();
            return sonuc;
        }
        [HttpDelete]
        [Route("api/derssil/{dersId}")]

        public SonucModel DersSil(int dersId)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == dersId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı";
                return sonuc;
            }
            if (db.Odev.Count(s => s.odevDersId == dersId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu derse kayıtlı ödev olduğu için silinemez";
                return sonuc;
            }
            db.Ders.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Silindi";
            return sonuc;
        }
        #endregion
        #region Odev
        [HttpGet]
        [Route("api/odevliste")]
        public List<OdevModel> OdevListele()
        {
            List<OdevModel> liste = db.Odev.Select(x => new OdevModel()
            {
                odevId = x.odevId,
                odevAdi = x.odevAdi,
                odevAciklama = x.odevAciklama,
                odevSonTarih = x.odevSonTarih,
                odevDersId = x.odevDersId,
                odevDersAdi = x.Ders.dersAdi
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/odevbyid/{odevId}")]
        public OdevModel OdevById(int odevId)
        {
            OdevModel kayit = db.Odev.Where(s => s.odevId == odevId).Select(x => new OdevModel()
            {
                odevId = x.odevId,
                odevAdi=x.odevAdi,
                odevAciklama = x.odevAciklama,
                odevSonTarih = x.odevSonTarih,
                odevDersId = x.odevDersId,
                odevDersAdi = x.Ders.dersAdi
            }).FirstOrDefault();
            return kayit;
        }



        [HttpPost]
        [Route("api/odevekle")]
        public SonucModel OdevEkle(OdevModel model)
        {
            if (db.Odev.Count(s => s.odevAdi == model.odevAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Kayıtlıdır";
                return sonuc;
            }
            Odev yeni = new Odev();
            yeni.odevAdi = model.odevAdi;
            yeni.odevAciklama = model.odevAciklama;
            yeni.odevSonTarih = model.odevSonTarih;
            yeni.odevDersId = model.odevDersId;
            db.Odev.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Ödev Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/odevduzenle")]
        public SonucModel OdevDuzenle(OdevModel model)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == model.odevId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Odev Bulunamadı";
                return sonuc;
            }
            kayit.odevAdi = model.odevAdi;
            kayit.odevAciklama = model.odevAciklama;
            kayit.odevSonTarih = model.odevSonTarih;
            kayit.odevDersId = model.odevDersId;
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Güncellendi";
            db.SaveChanges();

            return sonuc;
        }
        [HttpDelete]
        [Route("api/odevsil/{odevId}")]

        public SonucModel OdevSil(int odevId)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == odevId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitOdevId == odevId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev üzerinde öğrenci kaydı olduğu için bu ödev silinemez";
                return sonuc;
            }
            db.Odev.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi";
            return sonuc;
        }

        #endregion
        #region Kayit

        [HttpGet]
        [Route("api/kayitlistele")]
        public List<KayitModel> KayitListele()
        {
            List<KayitModel> liste = db.Kayit.Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOdevId = x.kayitOdevId,
                kayitUyeId = x.kayitUyeId,
                kayitDurum = x.kayitDurum,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.kayitUyeId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }
            return liste;
        }


        [HttpGet]
        [Route("api/uyeodevlistele/{uyeId}")]
        public List<KayitModel> UyeOdevListele(int uyeId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitUyeId == uyeId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOdevId = x.kayitOdevId,
                kayitUyeId = x.kayitUyeId,
                kayitDurum = x.kayitDurum,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.kayitUyeId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }
            return liste;
        }
        [HttpGet]
        [Route("api/odevuyelistele/{odevId}")]
        public List<KayitModel> OdevUyeListele(int odevId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOdevId == odevId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOdevId = x.kayitOdevId,
                kayitUyeId = x.kayitUyeId,
                kayitDurum = x.kayitDurum,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.kayitUyeId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }
            return liste;
        }
        [HttpGet]
        [Route("api/kayitlistelebydurum/{kayitDurum}")]
        public List<KayitModel> KayitListeleByDurum(int kayitDurum)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitDurum == kayitDurum).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitOdevId = x.kayitOdevId,
                kayitUyeId = x.kayitUyeId,
                kayitDurum = x.kayitDurum,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyeBilgi = UyeById(kayit.kayitUyeId);
                kayit.odevBilgi = OdevById(kayit.kayitOdevId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitOdevId == model.kayitOdevId && s.kayitUyeId == model.kayitUyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlgili öğrencinin ödevi kayıtlıdır";
                return sonuc;
            }
            Kayit yeni = new Kayit();
            yeni.kayitUyeId = model.kayitUyeId;
            yeni.kayitOdevId = model.kayitOdevId;
            yeni.kayitDurum = model.kayitDurum;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Gönderildi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(int kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi";
            return sonuc;
        }

        #endregion


    }
}
