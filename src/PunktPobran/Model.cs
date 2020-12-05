namespace PunktPobran
{

    public class Model
    {

        public int id_gsl_miej { get; set; }
        public string nazwa_swd { get; set; }
        public string adr_siedz_kod_poczt { get; set; }
        public string adr_siedz_miejsc { get; set; }
        public string adr_siedz_ulica { get; set; }
        public string adr_siedz_nr_domu { get; set; }
        public string adr_siedz_nr_lokalu { get; set; }
        public string nazwa_miejsca { get; set; }
        public string adr_lok_kod_poczt { get; set; }
        public string adr_lok_miejsc { get; set; }
        public string adr_lok_ulica { get; set; }
        public string adr_lok_nr_domu { get; set; }
        public string adr_lok_nr_lokalu { get; set; }
        public string telefon_rej { get; set; }
        public string mail { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string harm_pon { get; set; }
        public string harm_wt { get; set; }
        public string harm_sr { get; set; }
        public string harm_czw { get; set; }
        public string harm_pt { get; set; }
        public string harm_sob { get; set; }
        public string harm_niedz { get; set; }
        public string strona_www_podmiotu { get; set; }
        public string uwagi { get; set; }
        public string naglowek { get; set; }
        public string RzecznikPrawPacjentaSzpitalaPsychiatrycznego { get; set; }
        public string telefondoRzSzP { get; set; }
        public string AdreseMaildoRzSzP { get; set; }
        public string CentrumZdrowiaPsychicznego { get; set; }
        public string Województwo { get; set; }
        public string Powiat { get; set; }

        public override string ToString()
        {
            return Województwo;
        }
    }

}