using System.Collections.Generic;

namespace PunktPobran
{

    public class Model
    {

        public string nazwa_swd { get; set; }
        public string nazwa_miejsca { get; set; }
        public string adr_lok_kod_poczt { get; set; }
        public string adr_lok_miejsc { get; set; }
        public string adr_lok_ulica { get; set; }
        public string adr_lok_nr_domu { get; set; }
        public string adr_lok_nr_lokalu { get; set; }
        public string telefon_rej { get; set; }
        public string harm_pon { get; set; }
        public string harm_wt { get; set; }
        public string harm_sr { get; set; }
        public string harm_czw { get; set; }
        public string harm_pt { get; set; }
        public string harm_sob { get; set; }
        public string harm_niedz { get; set; }
        public string Województwo { get; set; }

        public override string ToString()
        {
            return Województwo;
        }
    }

    public class GroupedModel
    {
        public string Województwo { get; set; }
        public List<TownPoints> TownPointsList { get; set; }
        public bool visible = false;
    }

    public class TownPoints
    {
        public string City { get; set; }
        public List<Point> Points { get; set; }

        public bool visible = false;

    }

    public class Point
    {
        public string nazwa_swd { get; set; }
        public string adr_lok_kod_poczt { get; set; }
        public string adr_lok_miejsc { get; set; }
        public string adr_lok_ulica { get; set; }
        public string adr_lok_nr_domu { get; set; }
        public string adr_lok_nr_lokalu { get; set; }
        public string telefon_rej { get; set; }
        public string harm_pon { get; set; }
        public string harm_wt { get; set; }
        public string harm_sr { get; set; }
        public string harm_czw { get; set; }
        public string harm_pt { get; set; }
        public string harm_sob { get; set; }
        public string harm_niedz { get; set; }
    }

}