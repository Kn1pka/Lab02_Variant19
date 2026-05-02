using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Variant19
{
    public partial class Form1 : Form
    {
        private TextBox txtNumber, txtRange, txtVisualize;
        private Label lblResult;
        private ListBox listBoxArmstrong;
        private PictureBox pictureBoxArmstrong;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Lab02 - Вариант 19 (Числа Армстронга)";
            this.Size = new Size(1200, 900); // Увеличенный размер окна
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ==================== ЗАДАЧА 1 ====================
            GroupBox grpTask1 = new GroupBox
            {
                Text = "Задача 1: Проверка на число Армстронга",
                Location = new Point(15, 10),
                Size = new Size(380, 160)
            };
            txtNumber = new TextBox { Location = new Point(150, 30), Size = new Size(120, 23) };
            Button btnCheck = new Button { Text = "Проверить", Location = new Point(280, 28), Size = new Size(85, 28) };
            lblResult = new Label { Text = "Результат:", Location = new Point(15, 75), AutoSize = true, MaximumSize = new Size(350, 0) };
            btnCheck.Click += btnCheck_Click;

            grpTask1.Controls.Add(new Label { Text = "Введите число:", Location = new Point(15, 33), AutoSize = true });
            grpTask1.Controls.Add(txtNumber);
            grpTask1.Controls.Add(btnCheck);
            grpTask1.Controls.Add(lblResult);

            // ==================== ЗАДАЧА 2 ====================
            GroupBox grpTask2 = new GroupBox
            {
                Text = "Задача 2: Генератор чисел Армстронга [1, N]",
                Location = new Point(410, 10), // Рядом с Задачей 1
                Size = new Size(760, 160)
            };
            txtRange = new TextBox { Location = new Point(120, 30), Size = new Size(100, 23) };
            Button btnGenerate = new Button { Text = "Найти все", Location = new Point(235, 28), Size = new Size(100, 28) };
            listBoxArmstrong = new ListBox { Location = new Point(350, 30), Size = new Size(395, 115), ScrollAlwaysVisible = true };
            btnGenerate.Click += btnGenerate_Click;

            grpTask2.Controls.Add(new Label { Text = "Введите N:", Location = new Point(15, 33), AutoSize = true });
            grpTask2.Controls.Add(txtRange);
            grpTask2.Controls.Add(btnGenerate);
            grpTask2.Controls.Add(listBoxArmstrong);

            // ==================== ЗАДАЧА 3 ====================
            GroupBox grpTask3 = new GroupBox
            {
                Text = "Задача 3: Визуализация проверки",
                Location = new Point(15, 185), // Ниже Задач 1 и 2
                Size = new Size(1155, 680)
            };
            txtVisualize = new TextBox { Location = new Point(220, 30), Size = new Size(140, 23) };
            Button btnVisualize = new Button { Text = "Визуализировать", Location = new Point(375, 28), Size = new Size(130, 28) };
            pictureBoxArmstrong = new PictureBox
            {
                Location = new Point(15, 70),
                Size = new Size(1125, 595),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            btnVisualize.Click += btnVisualize_Click;
            pictureBoxArmstrong.Paint += pictureBoxArmstrong_Paint;

            grpTask3.Controls.Add(new Label { Text = "Число для визуализации:", Location = new Point(15, 33), AutoSize = true });
            grpTask3.Controls.Add(txtVisualize);
            grpTask3.Controls.Add(btnVisualize);
            grpTask3.Controls.Add(pictureBoxArmstrong);

            this.Controls.Add(grpTask1);
            this.Controls.Add(grpTask2);
            this.Controls.Add(grpTask3);
        }

        // 🔹 ЗАДАЧА 1: Проверка на число Армстронга
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNumber.Text, out int number) || number < 0)
            {
                lblResult.Text = "❌ Ошибка: введите целое неотрицательное число.";
                return;
            }

            string numStr = number.ToString();
            int digitsCount = numStr.Length;
            double sum = 0;

            foreach (char c in numStr)
            {
                int digit = c - '0';
                sum += Math.Pow(digit, digitsCount);
            }

            if ((int)sum == number)
                lblResult.Text = $"✅ Число {number} ЯВЛЯЕТСЯ числом Армстронга.";
            else
                lblResult.Text = $"❌ Число {number} НЕ является числом Армстронга.";
        }

        // 🔹 ЗАДАЧА 2: Генератор чисел Армстронга
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            listBoxArmstrong.Items.Clear();

            if (!int.TryParse(txtRange.Text, out int n) || n < 1)
            {
                MessageBox.Show("Введите N ≥ 1", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 1; i <= n; i++)
            {
                if (IsArmstrong(i))
                    listBoxArmstrong.Items.Add(i);
            }
        }

        private bool IsArmstrong(int number)
        {
            string numStr = number.ToString();
            int len = numStr.Length;
            double sum = 0;
            foreach (char c in numStr)
                sum += Math.Pow(c - '0', len);
            return (int)sum == number;
        }

        // 🔹 ЗАДАЧА 3: Визуализация в PictureBox
        private void btnVisualize_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVisualize.Text, out int number) || number < 0)
            {
                MessageBox.Show("Введите целое неотрицательное число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            pictureBoxArmstrong.Invalidate();
        }

        private void pictureBoxArmstrong_Paint(object sender, PaintEventArgs e)
        {
            if (!int.TryParse(txtVisualize.Text, out int number) || number < 0) return;

            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            string numStr = number.ToString();
            int digitsCount = numStr.Length;

            int startX = 80, startY = 50;
            int blockW = 80, blockH = 70, gap = 40;
            Font fDigit = new Font("Arial", 18, FontStyle.Bold);
            Font fSmall = new Font("Arial", 12, FontStyle.Regular);
            Font fTitle = new Font("Arial", 14, FontStyle.Bold);
            Pen pen = new Pen(Color.DimGray, 2);

            double sum = 0;
            int currentX = startX;

            for (int i = 0; i < digitsCount; i++)
            {
                int digit = int.Parse(numStr[i].ToString());
                double power = Math.Pow(digit, digitsCount);
                sum += power;

                g.FillRectangle(Brushes.LightSkyBlue, currentX, startY, blockW, blockH);
                g.DrawRectangle(Pens.SteelBlue, currentX, startY, blockW, blockH);
                g.DrawString(digit.ToString(), fDigit, Brushes.Black, currentX + 26, startY + 20);
                g.DrawString($"^{digitsCount}", fSmall, Brushes.DarkBlue, currentX + 8, startY + blockH - 20);
                g.DrawLine(pen, currentX + blockW / 2, startY + blockH + 2, currentX + blockW / 2, startY + blockH + 25);
                g.DrawString(power.ToString("0"), fSmall, Brushes.DarkGreen, currentX + 8, startY + blockH + 30);

                if (i < digitsCount - 1)
                {
                    int arrowY = startY + blockH + 55;
                    g.DrawLine(pen, currentX + blockW, arrowY, currentX + blockW + gap - 15, arrowY);
                    g.FillPolygon(Brushes.DimGray, new Point[] {
                        new Point(currentX + blockW + gap - 15, arrowY - 6),
                        new Point(currentX + blockW + gap - 15, arrowY + 6),
                        new Point(currentX + blockW + gap, arrowY)
                    });
                }

                currentX += blockW + gap;
            }

            int totalWidth = startX + (blockW + gap) * digitsCount - gap;
            int sumY = startY + blockH + 100;
            int sumBoxW = totalWidth - startX;

            g.DrawLine(pen, startX, sumY - 20, startX + sumBoxW, sumY - 20);

            string sumText = $"Сумма = {(int)sum}";
            SizeF sumSize = g.MeasureString(sumText, fTitle);
            g.FillRectangle(Brushes.PaleGreen, startX, sumY, (int)sumSize.Width + 40, 55);
            g.DrawRectangle(Pens.Green, startX, sumY, (int)sumSize.Width + 40, 55);
            g.DrawString(sumText, fTitle, Brushes.DarkGreen, startX + 15, sumY + 15);

            string resultText = (int)sum == number
                ? $"✅ {number} — ЧИСЛО АРМСТРОНГА"
                : $"❌ {number} — НЕ число Армстронга";
            Color resultColor = (int)sum == number ? Color.DarkGreen : Color.Crimson;

            g.DrawString(resultText, new Font("Arial", 18, FontStyle.Bold), new SolidBrush(resultColor), startX, sumY + 90);

            fDigit.Dispose(); fSmall.Dispose(); fTitle.Dispose(); pen.Dispose();
        }
    }
}