using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hari_6
{    
    public class Program
    {

        static void Main()
        {
            List<Film> listFilm = new List<Film>();
            listFilm.Add(new Film("Avanger", 40000, 120, 1, 15));
            listFilm.Add(new Film("Malignant", 35000, 90, 2, 15));
            listFilm.Add(new Film("Shang-Chi", 45000, 150, 3, 15));

            List<Admin> listAdmin = new List<Admin>();
            listAdmin.Add(new Admin("Afif Junihar Fakri","afifjunihar", "Bismillah"));
            MenuUtama(listFilm, listAdmin);
            
		}

        static void MenuUtama(List<Film> listFilm, List<Admin> listAdmin)
        {
            int exit = 0;
            try
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Selamat Datang di Bioskop XXI\n");
                    Console.WriteLine("=============================\n");
                    Console.WriteLine("Semoga Harimu Menyenangkan\n");
                    string[] menuUtama = { "1. Film Hari Ini", "2. Pesan Tiket", "3. Keluar", "0.Login Admin" };
                    foreach (string a in menuUtama)
                    {
                        Console.WriteLine(a);
                    }
                    Console.WriteLine("Masukan Pilihan Anda (hanya berupa angka)>>");
                    int pilih;
                    pilih = int.Parse(Console.ReadLine());
                    switch (pilih)
                    {
                        case 1:
                            DaftarFilm(listFilm, listAdmin, false);
                            break;
                        case 2:
                            PesanTiket(listFilm);
                            break;
                        case 3:
                            Environment.Exit(1);
                            Console.Clear();
                            break;
                        case 0:
                            MenuAdmin(listAdmin, listFilm);
                            break;
                    }
                }
                while (exit == 0);
            }
            catch (FormatException)
            {               
                Console.WriteLine("\nMasukan Input Berupa Angka");
                Console.ReadKey();
                MenuUtama(listFilm, listAdmin);
            }
            catch (IndexOutOfRangeException)
            {              
                Console.Write("\nMohon Masukan ID yang Benar");
                Console.ReadKey();
                MenuUtama(listFilm, listAdmin);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Write("\nMohon Masukan Data dengan Benar");
                Console.ReadKey();
                MenuUtama(listFilm, listAdmin);
            }
        }

        static void DaftarFilm(List<Film> listFilm,List<Admin> listAdmin, bool admin)
        {
            Console.Clear();
            Console.WriteLine("===============================\n");
            Console.WriteLine("===Film yang Tayang Hari Ini===\n");
            Console.WriteLine("===============================\n");
            int i = 0;
            foreach (var Film in listFilm)
            {
                i += 1;
                Console.WriteLine($"ID             : {i}");
                Console.WriteLine($"Judul Film     : {Film.nama}");
                Console.WriteLine($"Lama Tayangan  : {Film.lama} Menit");
                Console.WriteLine($"Harga Tiket    : Rp.{Film.hargaFilm},00");
                Console.WriteLine($"Sisa Kursi     : {Film.maksOrang}");
                Console.WriteLine("\n");
            }
            if (admin)
            {
                AdminMenuFilm(listAdmin, listFilm);
            }
            else 
            {
                Console.WriteLine("Tekan Apapun untuk Kembali ke Menu Utama >>");
                Console.ReadKey();
            }

        }
        public static void PesanTiket(List<Film> listFilm)
        {
            Console.Clear();
            Console.WriteLine("===============================\n");
            Console.WriteLine("======Silahkan Pilih Film======\n");
            Console.WriteLine("===============================\n");
            int i = 0;
            foreach (var Film in listFilm)
            {
                i += 1;
                Console.WriteLine($"ID             : {i}");
                Console.WriteLine($"Judul Film     : {Film.nama}");
                Console.WriteLine($"Harga Tiket    : {Film.hargaFilm}");
                Console.WriteLine("\n");
            }
            Console.WriteLine("Masukan Pilihan Anda >> \n");

            Console.Write("Nama Anda       : ");
            string pembeli = Console.ReadLine();

            Console.Write("ID Film         : ");
            int idFilm = int.Parse(Console.ReadLine())-1;

            Console.Write("Uang Anda       : Rp.");
            int uangPembeli = int.Parse(Console.ReadLine());

            if (uangPembeli >= listFilm[idFilm].hargaFilm)
            {
                int kembalian = listFilm[idFilm].BeliTiket(uangPembeli);
                Console.Write($"Kembalian Anda  : Rp.{kembalian}\n");
                Console.Write($"\nSilahkan Menenton Film {listFilm[idFilm - 1].nama} Anda pada Studio {listFilm[idFilm - 1].studio}\n");
                Console.Write("Selamat Menikmati Film Anda\n");
                listFilm[idFilm - 1].maksOrang = listFilm[idFilm - 1].maksOrang - 1;
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("\n");
                Console.Write("Mohon Bawa Uang yang Cukup \n");
                Console.Write("Semoga Hari Anda Menyenangkan\n");
                Console.ReadKey();
            }
        }
   
        static void MenuAdmin(List<Admin> listAdmin, List<Film> listFilm)
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("======Silahkan Pilih Menu======");
            Console.WriteLine("===============================");
            Console.WriteLine("1. Lihat ID Admin");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Keluar Menu Admin\n");
            Console.Write("Masukan Pilihan Anda [1/2/3] >>");

            int cekmenuadmin = int.Parse(Console.ReadLine());

            if (cekmenuadmin == 1)
            {
                LihatIdAdmin(listAdmin, listFilm);
            }

            else if (cekmenuadmin == 2)
            {
                LoginAdmin(listAdmin, listFilm);
            }

            else if (cekmenuadmin == 3)
            {
                MenuUtama(listFilm, listAdmin);
            }

            else 
            {
                Console.Write("Mohon Masukan Input yang Sesuai");
                Console.ReadKey();
                MenuAdmin(listAdmin, listFilm);
            }
           
        }

        static void LoginAdmin(List<Admin> listAdmin, List<Film> listFilm)
        {
            Console.Clear();
            Console.Write("Mohon Masukan username, dan password Anda\n");

            Console.Write("Username\t     : ");
            string userName = Console.ReadLine();
            Console.Write("Password\t     : ");

            string pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            }
            while (key != ConsoleKey.Enter);


            bool exist = listAdmin.Exists(item => item.unameAdmin == userName);
            if (exist) 
            {
                bool passBenar = BCrypt.Net.BCrypt.Verify(pass, listAdmin[listAdmin.FindIndex(user => user.unameAdmin == userName)].passwordAdmin);
                
                if (passBenar)
                {
                    AdminMenuFilm(listAdmin, listFilm);
                }
                else
                {
                    Console.WriteLine("\n=============================");
                    Console.WriteLine("Gagal Login: Password yang Anda masukkan salah");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\n=============================");
                Console.WriteLine("Gagal Login: Username tidak terdaftar");
                Console.ReadKey();
            }           
        }

        static void AdminMenuFilm(List<Admin> listAdmin, List<Film> listFilm)
        {
            Console.Clear();
            Console.WriteLine("Selamat Datang Admin");
            Console.WriteLine("1. Tambah Film ");
            Console.WriteLine("2. Hapus Film");
            Console.WriteLine("3. Edit Film");
            Console.WriteLine("4. Lihat ID Fim");
            Console.WriteLine("5. Keluar Menu Admin");
            Console.Write("Masukan Pilihan Anda :");
            int menuadmin = int.Parse(Console.ReadLine());
            switch (menuadmin)
            {
                case 1: TambahFilm(listFilm, listAdmin); break;
                case 2: HapusFilm(listFilm, listAdmin); break;
                case 3: EditFilm(listFilm, listAdmin); break;
                case 4: DaftarFilm(listFilm, listAdmin, true);break;
                case 5: MenuUtama(listFilm, listAdmin); break;
            }
        }

        static void LihatIdAdmin(List<Admin> listAdmin, List<Film> listFilm)
        {
            Console.Clear();
            
            foreach (var c in listAdmin)
            {   
                
                int i = 0;
                Console.WriteLine($"ID             : {i}");
                Console.WriteLine($"Nama Admin     : {c.fullName}");
                i += 1;
            }

            Console.ReadKey();
            MenuAdmin(listAdmin, listFilm);

        }


        static void TambahFilm(List<Film> listFilm, List<Admin> listAdmin)
        {
            try
            {
                Console.Clear();
                Console.Write("Masukan Judul Film Baru              : ");

                string judul = Console.ReadLine();
                Console.Write("Masukan harga Film [hanya angka]             : Rp. ");
                int hargaTiket = int.Parse(Console.ReadLine());
                Console.Write("Masukan lama Film (menit) [hanya angka]      : ");
                int lamaPenayangan = int.Parse(Console.ReadLine());
                Console.Write("Masukan Studio Penayangan Film [hanya angka] : ");
                int studio = int.Parse(Console.ReadLine());
                Console.WriteLine("Masukan maks orang [hanya angka]         : ");
                int orang = int.Parse(Console.ReadLine());
                listFilm.Add(new Film(judul, hargaTiket, lamaPenayangan, studio, orang));
                Console.WriteLine("Film Berhasil Terdaftar !\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Film Gagal Terdaftar !\n");
                Console.ReadKey();
                AdminMenuFilm(listAdmin, listFilm);
            }
        }

        static void HapusFilm(List<Film> listFilm, List<Admin> listAdmin)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Masukan ID Film yang mau dihapus >> \n");
                Console.WriteLine("0. Cancel");
              
                int idFilm = int.Parse(Console.ReadLine());
                if (idFilm == 0) 
                {
                    MenuAdmin(listAdmin, listFilm);
                }
                else 
                { 
                    listFilm.RemoveAt(idFilm - 1);
                    Console.WriteLine($"Film {listFilm[idFilm].nama} Berhasil di Hapus");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Mohon masukan ID Film yang valid");
                Console.ReadKey();
                AdminMenuFilm(listAdmin, listFilm);
            }

        }

        static void EditFilm(List<Film> listFilm, List<Admin> listAdmin)
        {
            Console.Clear();

            try
            {
                Console.WriteLine("Masukan ID Film yang mau diubah >> \n");
                int id = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine($"Film yang akan Diubah adalah {listFilm[id].nama}");

                Console.WriteLine("Masukan Revisi Judul      : \n");
                listFilm[id].nama = Console.ReadLine();
                Console.WriteLine("Masukan Lama Penayangan   : \n");
                listFilm[id].lama = int.Parse(Console.ReadLine());

                Console.WriteLine("Masukan hargaFilm Tiket       : \n");
                listFilm[id].hargaFilm = int.Parse(Console.ReadLine());

                Console.WriteLine("Masukan Studio Penayangan : \n");
                listFilm[id].studio = int.Parse(Console.ReadLine());
                Console.WriteLine("Masukan Kursi Penonton    : \n");
                listFilm[id].maksOrang = int.Parse(Console.ReadLine());
                Console.WriteLine("Film Sudah diubah \n");
            }
            catch (Exception)
            {
                Console.WriteLine("Film Gagal diubah \n");
                Console.ReadKey();
                AdminMenuFilm(listAdmin, listFilm);
            }
           

        }

    }
}
