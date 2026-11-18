using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class calculator : Form
    {
        // Переменные для логики
        private double _result = 0;
        private string _currentOperation = string.Empty;
        private bool _isNewNumber = true;
        public calculator()
        {
            InitializeComponent();
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void calculator_Click(object sender, EventArgs e)
        {
Button button = (Button)sender;

if (txtDisplay.Text == "0" || _isNewNumber)
{
    txtDisplay.Text = button.Text;
    _isNewNumber = false;
}
else
{
    txtDisplay.Text += button.Text;
}
        }

        private void Operation_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Если пользователь нажал +, а потом еще раз +, не сбиваем логику
            if (_currentOperation != string.Empty && !_isNewNumber)
            {
                btnEquals.PerformClick();
            }

            _currentOperation = button.Text; // Запоминаем, что хотим сложить или вычесть
            _result = double.Parse(txtDisplay.Text.Replace(',', '.')); // Сохраняем число из дисплея
            _isNewNumber = true; // Говорим системе, что следующее число будет новым
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            _result = 0;
            _currentOperation = string.Empty;
            _isNewNumber = true;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {

        }

        private void Number_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Файл создастся автоматически в папке с программой (bin/Debug)
                string filePath = "calc_history.txt";
                // Дозаписываем результат с датой и временем
                System.IO.File.AppendAllText(filePath, $"{DateTime.Now}: {txtDisplay.Text}{Environment.NewLine}");
                MessageBox.Show("Результат сохранен в файл calc_history.txt", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = "calc_history.txt";
                if (System.IO.File.Exists(filePath))
                {
                    // Читаем все строки и берем самую последнюю
                    var lines = System.IO.File.ReadAllLines(filePath);
                    if (lines.Length > 0)
                    {
                        string lastLine = lines[lines.Length - 1];
                        // Парсим только число после двоеточия
                        string value = lastLine.Split(':')[1].Trim();
                        txtDisplay.Text = value;
                        _result = double.Parse(value.Replace(',', '.'));
                        _isNewNumber = true;
                    }
                }
                else
                {
                    MessageBox.Show("Файл истории не найден. Сначала сохраните результат.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTheme.SelectedItem == null) return;

            switch (cmbTheme.SelectedItem.ToString())
            {
                case "Светлая":
                    this.BackColor = SystemColors.Control;
                    txtDisplay.BackColor = Color.White;
                    txtDisplay.ForeColor = Color.Black;
                    break;

                case "Темная":
                    this.BackColor = Color.FromArgb(45, 45, 48);
                    txtDisplay.BackColor = Color.FromArgb(30, 30, 33);
                    txtDisplay.ForeColor = Color.White;
                    break;

                case "Неоновая":
                    this.BackColor = Color.Black;
                    txtDisplay.BackColor = Color.Black;
                    txtDisplay.ForeColor = Color.Lime;
                    break;
            }

            // Применяем выбранный цвет ко всем кнопкам на форме
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    // Чтобы текст на кнопках был виден в Темной теме
                    if (cmbTheme.SelectedItem.ToString() == "Темная")
                    {
                        control.BackColor = Color.FromArgb(60, 60, 63);
                        control.ForeColor = Color.White;
                    }
                    else if (cmbTheme.SelectedItem.ToString() == "Неоновая")
                    {
                        control.BackColor = Color.Black;
                        control.ForeColor = Color.Lime;
                    }
                    else // Светлая
                    {
                        control.BackColor = SystemColors.Control;
                        control.ForeColor = Color.Black;
                    }
                }
            }
        }        

        }
    }
    
    
    
    
    

