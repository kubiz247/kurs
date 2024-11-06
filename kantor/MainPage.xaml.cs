using System.Net;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace kantor
{
    public partial class MainPage : ContentPage
    {

        /******************************************************* 

        nazwa funkcji:        <Currency> 

        parametry wejściowe:  <table> - <tabela danych, pobiera i oddaje wartosci>
                              <currency> - <waluta, pobiera i oddaje wartosci>
                              <code> - <kod waluty, pobiera i oddaje wartosci>
                              <rates> - <kursy, pobiera i oddaje wartosci>

        wartość zwracana:     <> 

        informacje:           <> 

        autor:                <Jakub> 

        ****************************************************/
        public class Currency()
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }
        }
        /******************************************************* 

        nazwa funkcji:        <Rate> 

        parametry wejściowe:  <no> - <noł, pobiera i oddaje wartosci>
                              <effectiveData> - <efektywna data, pobiera i oddaje wartosci>
                              <bid> - <kup, pobiera i oddaje wartosci>
                              <ask> - <sprzedaj, pobiera i oddaje wartosci>

        wartość zwracana:     <> 

        informacje:           <klasa ktora oblicza roznice na rynku> 

        autor:                <Jakub> 

        ****************************************************/
        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveData { get; set; }
            public string? bid { get; set; }
            public string? ask { get; set; }
        }
        public MainPage()
        {
            InitializeComponent();
        }
        /******************************************************* 

        nazwa funkcji:        <OnCounterClicked> 

        parametry wejściowe:  <Date> - <data, aktualna data>
                              <usd> - <link do json>
                              <usd2> - <drugi link do json>
                              <usdj> - <sluzy jako odwolanie do deserializacji przy Currency usdc>
                              <usdj2> - <sluzy jako odwolanie do deserializacji przy Currency usdc2>
                              <number1> - <parsuje tekst>
                              <InitialNumber> - <pierwotny numer>
                              <convertedNumber> - <przekonwertowany numer>

        wartość zwracana:     <> 

        informacje:           <tutaj wiekszosc odpowiada za waluty> 

        autor:                <Jakub> 

        ****************************************************/
        private void OnCounterClicked(object sender, EventArgs e)
        {
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            string usd = "https://api.nbp.pl/api/exchangerates/rates/c/" + fe.Text + "/" + Date + "/?format=json";
            string usd2 = "https://api.nbp.pl/api/exchangerates/rates/c/" + se.Text + "/" + Date + "/?format=json";

            string usdj;
            string usdj2;
            using (var webClient = new WebClient())
            {
                usdj = webClient.DownloadString(usd);
                usdj2 = webClient.DownloadString(usd2);
            }

            Currency usdc = JsonSerializer.Deserialize<Currency>(usdj);
            Currency usdc2 = JsonSerializer.Deserialize<Currency>(usdj2);
            double number1 = double.Parse(pr.Text);

            double InitialNumber = number1 * usdc.rates[0].ask;
            double convertedNumber = InitialNumber / usdc2.rates[0].ask;
            wy.Text = convertedNumber.ToString();
        }
    }
}
