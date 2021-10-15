using System;
using System.Collections.Generic;
using System.Text;

namespace Hari_6
{
    public class Film : MenuTiket
    {
        public string nama;
        private int harga;
        public int hargaFilm
        {
            get
            {
                return harga;
            }

            set
            {
                harga = value;
            }
        }

        public int lama;
        public int studio;
        public int maksOrang;
        public Film(string nama, int harga, int lama, int studio, int maksOrang)
        {
            this.nama = nama;
            this.harga = harga;
            this.lama = lama;
            this.studio = studio;
            this.maksOrang = maksOrang;
        }

        public override int BeliTiket(int uangPembeli)
        {
            return (uangPembeli - hargaFilm);
        }

        public override int BeliTiket(float uangPembeli)
        {
            return (Convert.ToInt32(uangPembeli) - hargaFilm);
        }

    }
}
