﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Sprav
{
    /// <summary>
    /// Логика взаимодействия для AbonentiOtchet.xaml
    /// </summary>
    public partial class AbonentiOtchet : Window
    {
        private Абоненты _abonent = new Абоненты();
        private Служебные_телефоны _slyg = new Служебные_телефоны();
        private Города_и_населенные_пункты _gorod = new Города_и_населенные_пункты();
        public AbonentiOtchet()
        {
            InitializeComponent();
            Vibor_g.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList();
        }

        public void CreateReport(string selectedCity)
        {
            // Создаем новый файл DOCX
            string reportFileName = "Отчет_по_абонентам_в_городе.docx";
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(reportFileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Добавляем заголовок
                DocumentFormat.OpenXml.Wordprocessing.Paragraph titleParagraph = body.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Paragraph());
                DocumentFormat.OpenXml.Wordprocessing.Run titleRun = titleParagraph.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Run());
                titleRun.AppendChild(new Text($"Отчет об абонентах в городе {selectedCity}"));
                titleParagraph.AppendChild(new Break()); // Добавляем разрыв строки

                // Получаем абонентов выбранного города
                var abonentsInCity = TelephonesEntities.GetContext().Абоненты.Where(a => a.Города_и_населенные_пункты.Название_населенного_пункта == selectedCity).ToList();

                // Добавляем информацию об абонентах
                foreach (var abonent in abonentsInCity)
                {
                    DocumentFormat.OpenXml.Wordprocessing.Paragraph abonentParagraph = body.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Paragraph());
                    DocumentFormat.OpenXml.Wordprocessing.Run abonentRun = abonentParagraph.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Run());
                    abonentRun.AppendChild(new Text($"ФИО: {abonent.Фамилия} {abonent.Имя} {abonent.Отчество}, Адрес: {abonent.Адрес}, Номер телефона: {abonent.Номер_телефона}, Город: {abonent.Города_и_населенные_пункты.Название_населенного_пункта}"));
                    abonentParagraph.AppendChild(new Break());
                }
            }


            MessageBox.Show("Отчет создан успешно");
            Process.Start(reportFileName);
        }

        private void Pechat_Click(object sender, RoutedEventArgs e)
        {
            if (Vibor_g.SelectedItem != null)
            {
                Города_и_населенные_пункты selectedCity = (Города_и_населенные_пункты)Vibor_g.SelectedItem;
                string cityName = selectedCity.Название_населенного_пункта;

                // Вызываем метод CreateReport для создания отчета
                CreateReport(cityName);
            }
            else
            {
                MessageBox.Show("Выберите город перед созданием отчета.");
            }
        }
    }
}
