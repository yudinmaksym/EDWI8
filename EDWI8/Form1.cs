using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDWI8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[,] array2D = new string[5,2];
            string item = textBox1.Text;
            string namefirstitem="";
            string pricefirstitem="";
            string nameseconditem = "";
            string priceseconditem = "";
            string namethirditem = "";
            string pricethirditem = "";
            string namefouthitem = "";
            string pricefouthitem = "";
            string namefifthitem = "";
            string pricefifthitem = "";

            string saturn;
            string eurocom;
            string neonet;
            string mediamarket;
            string oleole;

            List<string> allshops = new List<string>();

            if (textBox1.Text == "")
            {
                saturn = "https://saturn.pl/rtv-i-telewizory/telewizory?page=1&sort=price_asc&limit=20";
                eurocom = "http://www.euro.com.pl/telewizory-led-lcd-plazmowe,d3.bhtml";
                neonet = "https://www.neonet.pl/telewizory.html?dir=asc&order=price";
                mediamarket = "https://mediamarkt.pl/rtv-i-telewizory/telewizory?page=1&sort=price_asc&limit=20";
                oleole = "http://www.oleole.pl/telewizory-led-lcd-plazmowe,d3.bhtml";
                allshops.Add(saturn);
                allshops.Add(eurocom);
                allshops.Add(neonet);
                allshops.Add(mediamarket);
                allshops.Add(oleole);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(saturn);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(eurocom);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(neonet);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(mediamarket);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(oleole);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
            }
            else
            {
                string splititem = item.Replace(' ', '-');
                string splitformediaexpert = item.Replace(' ', '+');
                string splitforallegro = item.Replace(" ", "%20");
                saturn = "https://saturn.pl/search?page=1&sort=price_asc&limit=20&query%5Bmenu_item%5D=10003&query%5Bquerystring%5D=" + splitformediaexpert + "&product_list_template=";
                eurocom = "http://www.euro.com.pl/search/telewizory-led-lcd-plazmowe,d3.bhtml?keyword=" + splitforallegro;
                neonet = "http://www.mycenter.pl/telewizory-plazmowe-lcd-led-stoliki?query="+ splitformediaexpert;
                mediamarket = "https://mediamarkt.pl/search?page=1&sort=price_asc&limit=20&query%5Bmenu_item%5D=25419&query%5Bquerystring%5D=" + splitformediaexpert + "&product_list_template=";
                oleole = "http://www.oleole.pl/search/telewizory-led-lcd-plazmowe,d3.bhtml?keyword="+ splitforallegro;
                allshops.Add(saturn);
                allshops.Add(eurocom);
                allshops.Add(neonet);
                allshops.Add(mediamarket);
                allshops.Add(oleole);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(saturn);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(eurocom);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(neonet);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(mediamarket);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(oleole);
                richTextBox1.AppendText(System.Environment.NewLine);
                richTextBox1.AppendText(System.Environment.NewLine);
            }

            for(int i = 0; i < allshops.Count; i++)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(allshops[i]);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;

                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(receiveStream);
                        }
                        else
                        {
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding("Windows-1250"));
                        }

                        string data2 = readStream.ReadToEnd();
                        //string data;

                        if (i == 0)
                        {
                            int start = data2.IndexOf("m-productsBox_photo") + 1;
                            int end = data2.IndexOf("</a>", start);

                            string text = data2.Substring(start, end - start);
                            text = Regex.Replace(text, @"\t+", "\n");
                            string[] item_properties = text.Split('\n');

                            for (int j = 0; j < item_properties.Length; j++)
                            {
                                if (item_properties[j].Contains("title") == true)
                                {

                                    namefirstitem = item_properties[j];
                                    int start3 = namefirstitem.IndexOf("\"") + 1;
                                    int end3 = namefirstitem.IndexOf("\"", start3);
                                    namefirstitem = namefirstitem.Substring(start3, end3 - start3);

                                }
                                if (item_properties[j].Contains("data-offer-price") == true)
                                {
                                    pricefirstitem = item_properties[j];
                                    int start3 = pricefirstitem.IndexOf("\"") + 1;
                                    int end3 = pricefirstitem.IndexOf("\"", start3);
                                    pricefirstitem = pricefirstitem.Substring(start3, end3 - start3);
                                }
                            }

                            //richTextBox1.AppendText("Saturn: " + namefirstitem + " -" + pricefirstitem + " pln");
                            //richTextBox1.AppendText(System.Environment.NewLine);
                        }

                        if (i == 1)
                        {
                            int start = data2.IndexOf("UA && UA.addProduct") + 1;
                            int end = data2.IndexOf("})", start);

                            string text = data2.Substring(start, end - start);
                            //text = Regex.Replace(text, @"\t+", "\n");
                            string[] item_properties = text.Split('\n');
                            for (int j = 0; j < item_properties.Length; j++)
                            {
                                if (item_properties[j].Contains("name") == true)
                                {

                                    nameseconditem = item_properties[j];
                                    int start3 = nameseconditem.IndexOf("\"") + 1;
                                    int end3 = nameseconditem.IndexOf("\"", start3);
                                    nameseconditem = nameseconditem.Substring(start3, end3 - start3);

                                }
                                if (item_properties[j].Contains("price") == true)
                                {
                                    priceseconditem = item_properties[j];
                                    int start3 = priceseconditem.IndexOf("\"") + 1;
                                    int end3 = priceseconditem.IndexOf("\"", start3);
                                    priceseconditem = priceseconditem.Substring(start3, end3 - start3);
                                }
                            }
                            //richTextBox1.AppendText("Eurocom: " + nameseconditem + " -" + priceseconditem + " pln");
                            //richTextBox1.AppendText(System.Environment.NewLine);
                        }

                        if (i == 2)
                        {
                            int start = data2.IndexOf("prd_content") + 1;
                            int end = data2.IndexOf("m-emblem_relative", start);

                            string text = data2.Substring(start, end - start);
                            text = Regex.Replace(text, @"\t+", "\n");
                            string[] item_properties = text.Split('\n');

                            for (int j = 0; j < item_properties.Length; j++)
                            {
                                if (item_properties[j].Contains("alt") == true)
                                {

                                    namethirditem = item_properties[j];
                                    int start3 = namethirditem.IndexOf("alt=\"") + 5;
                                    int end3 = namethirditem.IndexOf("\"", start3);
                                    namethirditem = namethirditem.Substring(start3, end3 - start3);

                                }
                                if (item_properties[j].Contains("price_type1") == true)
                                {
                                    pricethirditem = item_properties[j];
                                    int start3 = pricethirditem.IndexOf(">") + 1;
                                    int end3 = pricethirditem.IndexOf("<", start3);
                                    pricethirditem = pricethirditem.Substring(start3, end3 - start3);
                                }
                            }
                            //richTextBox1.AppendText("Mycenter.pl: " + namethirditem + " -" + pricethirditem + " pln");
                            //richTextBox1.AppendText(System.Environment.NewLine);
                        }

                        if (i == 3)
                        {
                            int start = data2.IndexOf("m-productsBox_photo") + 1;
                            int end = data2.IndexOf("</a>", start);

                            string text = data2.Substring(start, end - start);
                            text = Regex.Replace(text, @"\t+", "\n");
                            string[] item_properties = text.Split('\n');

                            for (int j = 0; j < item_properties.Length; j++)
                            {
                                if (item_properties[j].Contains("title") == true)
                                {

                                    namefouthitem = item_properties[j];
                                    int start3 = namefouthitem.IndexOf("\"") + 1;
                                    int end3 = namefouthitem.IndexOf("\"", start3);
                                    namefouthitem = namefouthitem.Substring(start3, end3 - start3);

                                }
                                if (item_properties[j].Contains("data-offer-price") == true)
                                {
                                    pricefouthitem = item_properties[j];
                                    int start3 = pricefouthitem.IndexOf("\"") + 1;
                                    int end3 = pricefouthitem.IndexOf("\"", start3);
                                    pricefouthitem = pricefouthitem.Substring(start3, end3 - start3);
                                }
                            }

                            //richTextBox1.AppendText("MediaMarket: " + namefouthitem + " -" + pricefouthitem + " pln");
                            //richTextBox1.AppendText(System.Environment.NewLine);
                        }

                        if (i == 4)
                        {
                            int start = data2.IndexOf("UA && UA.addProduct") + 1;
                            int end = data2.IndexOf("})", start);

                            string text = data2.Substring(start, end - start);
                            //text = Regex.Replace(text, @"\t+", "\n");
                            string[] item_properties = text.Split('\n');
                            for (int j = 0; j < item_properties.Length; j++)
                            {
                                if (item_properties[j].Contains("name") == true)
                                {

                                    namefifthitem = item_properties[j];
                                    int start3 = namefifthitem.IndexOf("\"") + 1;
                                    int end3 = namefifthitem.IndexOf("\"", start3);
                                    namefifthitem = namefifthitem.Substring(start3, end3 - start3);

                                }
                                if (item_properties[j].Contains("price") == true)
                                {
                                    pricefifthitem = item_properties[j];
                                    int start3 = pricefifthitem.IndexOf("\"") + 1;
                                    int end3 = pricefifthitem.IndexOf("\"", start3);
                                    pricefifthitem = pricefifthitem.Substring(start3, end3 - start3);
                                }
                            }
                            //richTextBox1.AppendText("OleOle: " + namefifthitem + " -" + pricefifthitem + " pln");
                            //richTextBox1.AppendText(System.Environment.NewLine);

                        }
                            
                    }

                }
                catch
                {

                }
            }
            int count = 0;
            try
            {
                string[] testik = pricefirstitem.Split('.');
                double first = Convert.ToDouble(testik[0]);
                array2D[count,0] =  "Saturn: " + namefirstitem;
                array2D[count, 1] = Convert.ToString(first);
                count++;
            }
            catch
            {

            }
            try
            {
                string[] testik2 = priceseconditem.Split('.');
                double second = Convert.ToDouble(testik2[0]);
                array2D[count, 0] = "Eurocom: " + nameseconditem;
                array2D[count, 1] = Convert.ToString(second);
                count++;
            }
            catch
            {

            }
            try
            {
                string[] testik3 = pricethirditem.Split('.');
                double third = Convert.ToDouble(testik3[0]);
                array2D[count, 0] = "Mycenter: " + namethirditem;
                array2D[count, 1] = Convert.ToString(third);
                count++;
            }
            catch
            {

            }
            try
            {
                string[] testik4 = pricefouthitem.Split('.');
                double fouth = Convert.ToDouble(testik4[0]);
                array2D[count, 0] = "MediaMarkt: " + namefouthitem;
                array2D[count, 1] = Convert.ToString(fouth);
                count++;
            }
            catch
            {

            }
            try
            {
                string[] testik5 = pricefifthitem.Split('.');
                double fifth = Convert.ToDouble(testik5[0]);
                array2D[count, 0] = "OleOle: " + namefifthitem;
                array2D[count, 1] = Convert.ToString(fifth);
                count++;
            }
            catch
            {

            }




            //array2D = new string[,] { { "Saturn: " + namefirstitem, Convert.ToString(first) }, { "Eurocom: " + nameseconditem, Convert.ToString(second) }, { "Mycenter: " + namethirditem, Convert.ToString(third) }, { "MediaMarkt: " + namefouthitem, Convert.ToString(fouth) }, { "OleOle: " + namefifthitem, Convert.ToString(fifth) } };
            //}
            //catch
            //{

            //}

            if (count < 3)
            {
                for (int i = 0; i < count; i++)
                {
                    richTextBox1.AppendText(System.Environment.NewLine);
                    richTextBox1.AppendText(array2D[i, 0] + ": " + array2D[i, 1] + " pln");
                }
            }
            else
            {
                for (int i = 0; i < array2D.Length; i++)
                {
                    for (int j = 0; j < count-1; j++)
                    {
                        if (Convert.ToInt32(array2D[j, 1]) > Convert.ToInt32(array2D[j + 1, 1]))
                        {
                            string z = array2D[j, 1];
                            string y = array2D[j, 0];
                            array2D[j, 1] = array2D[j + 1, 1];
                            array2D[j, 0] = array2D[j + 1, 0];
                            array2D[j + 1, 1] = z;
                            array2D[j + 1, 0] = y;
                        }
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    richTextBox1.AppendText(System.Environment.NewLine);
                    richTextBox1.AppendText(array2D[i, 0] + ": " + array2D[i, 1] + " pln");
                }
            }
            

        }
    }
}
   
