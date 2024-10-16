using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trpo_automat
{
    public partial class Main : Form
    {
        private string текущийНапиток1;
        private int сахарКоличество = 0;
        private int внесенныеДеньги = 0;
        private Dictionary<string, int> напиткиЦены = new Dictionary<string, int>
        {
            {"Американо", 100},
            {"Кипяток", 30},
            {"Ирландский кофе", 140},
            {"Двойной шоколад", 65},
            {"Чай черный", 40},
            {"Мокка", 140},
            {"Капучино", 100},
            {"Двойной эспрессо", 90},
            {"Горячий шоколад", 60},
            {"Чай зеленый", 45},
            {"Латте", 100},
            {"Эспрессо", 85}
        };
        public Main()
        {
            InitializeComponent();

            Картинка_сдача.Visible = false;
            Каптинка_напиток.Visible = false;


            this.StartPosition = FormStartPosition.CenterScreen;

            Сахар.Text = "Сахар: " + сахарКоличество + " кубиков";

            Монеты.Items.AddRange(new string[] { "10 р.", "5 р.", "1 р." });
            Купюры.Items.AddRange(new string[] { "500 р.", "200 р.", "100 р.", "50 р.", "10 р." });

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Деньги_Click(object sender, EventArgs e)
        {

        }

        private void Сахар_Click(object sender, EventArgs e)
        {

        }

        private void Плюс_Click(object sender, EventArgs e)
        {
            if (сахарКоличество < 4)
            {
                сахарКоличество++;
                ОбновитьСахар();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (сахарКоличество > 0)
            {
                сахарКоличество--;
                ОбновитьСахар();
            }
        }

        private void ОбновитьСахар()
        {
            Сахар.Text = "Сахар: " + сахарКоличество + " кубиков";
        }


        private void Монеты_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (Монеты.SelectedItem.ToString())
            {
                case "10 р.": внесенныеДеньги += 10; break;
                case "5 р.": внесенныеДеньги += 5; break;
                case "1 р.": внесенныеДеньги += 1; break;
            }
            ОбновитьДеньги();
        }

        private void Купюры_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Купюры.SelectedItem.ToString())
            {
                case "500 р.": внесенныеДеньги += 500; break;
                case "200 р.": внесенныеДеньги += 200; break;
                case "100 р.": внесенныеДеньги += 100; break;
                case "50 р.": внесенныеДеньги += 50; break;
                case "10 р.": внесенныеДеньги += 10; break;
            }

            ОбновитьДеньги();

        }

        private void Картинка_сдача_Click(object sender, EventArgs e)
        {

        }

        private void ОбновитьДеньги()
        {
            Деньги.Text = "Cумма: " + внесенныеДеньги + " р.";
        }
        private bool РассчитатьСдачу(string выбранныйНапиток)
        {
            if (!напиткиЦены.ContainsKey(выбранныйНапиток))
            {
                MessageBox.Show("Выбранный напиток не найден.");
                return false;
            }

            int цена = напиткиЦены[выбранныйНапиток];

            if (внесенныеДеньги >= цена)
            {
                int сдача = внесенныеДеньги - цена;
                Деньги.Text = $"Сдача: {сдача} р.";
                return true;
            }

            int нехватка = цена - внесенныеДеньги;
            Деньги.Text = $"Недостаточно средств. Не хватает: {нехватка} р.";

            return false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (РассчитатьСдачу(текущийНапиток1))
            {
                MessageBox.Show($"Для вас готовится {текущийНапиток1} ... ");
                Картинка_сдача.Visible = true;
                Каптинка_напиток.Visible = true;
            }
            else
            {
                MessageBox.Show($"У вас недостаточно средств на {текущийНапиток1}");
            }
        }

        private void Каптинка_напиток_DoubleClick(object sender, EventArgs e)
        {

            MessageBox.Show("Вы забрали напиток!");
            сахарКоличество = 0;
            внесенныеДеньги = 0;
            Деньги.Text = "Cумма: " + внесенныеДеньги + " р.";
            Сахар.Text = "Сахар: " + сахарКоличество + " кубиков";

            Картинка_сдача.Visible = false;
            Каптинка_напиток.Visible = false;


        }

        private void Напиток(object sender, EventArgs e)
        {

            RadioButton selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton != null && selectedRadioButton.Checked)
            {
               
                string[] parts = selectedRadioButton.Text.Split(' ');
                if (parts.Length > 1)
                {
                    текущийНапиток1 = string.Join(" ", parts, 0, parts.Length - 1); // Join all but the last part
                }
                else
                {
                    текущийНапиток1 = parts[0]; // Fallback in case there's no space
                }

                РассчитатьСдачу(текущийНапиток1); // Call to calculate change
            }
        }
    }
}
