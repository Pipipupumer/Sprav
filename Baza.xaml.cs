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

namespace Sprav
{
    /// <summary>
    /// Логика взаимодействия для Baza.xaml
    /// </summary>
    public partial class Baza : Window
    {
        private Абоненты _abonents = new Абоненты();
        private Служебные_телефоны _slygi = new Служебные_телефоны();
        private Города_и_населенные_пункты _goroda = new Города_и_населенные_пункты();
        public Baza()
        {
            InitializeComponent();
            DataContext = _abonents;
            Abonenti.ItemsSource = TelephonesEntities.GetContext().Абоненты.ToList();
            DGridSlyg.ItemsSource = TelephonesEntities.GetContext().Служебные_телефоны.ToList();
            DgridGoroda.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList();
            Vibor_goroda_slygi.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList();
            Vibor_goroda.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList() ;
        }
        public void HideBorder()
        {
            Settings.Visibility = Visibility.Collapsed;
            SettingsSlyg.Visibility = Visibility.Collapsed;
            SettingsCity.Visibility = Visibility.Collapsed; 
        }
        private void AddnewAbonent_Click(object sender, RoutedEventArgs e)
        {
            if (_abonents.ID == 0)
                TelephonesEntities.GetContext().Абоненты.Add(_abonents);
            try
            {
    
                TelephonesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Abonenti.ItemsSource = TelephonesEntities.GetContext().Абоненты.ToList();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Abonenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Abonenti.SelectedItem != null)
            {
                // Получить выбранный элемент из DataGrid
                var selectedAbonent = Abonenti.SelectedItem as Абоненты;

                // Установить свойства выбранного элемента как текст для соответствующих TextBox
                F.Text = selectedAbonent.Фамилия;
                I.Text = selectedAbonent.Имя;
                O.Text = selectedAbonent.Отчество;
                Adress.Text = selectedAbonent.Адрес;
                Phonenumber.Text = selectedAbonent.Номер_телефона?.ToString();
                Vibor_goroda.SelectedItem = selectedAbonent.Города_и_населенные_пункты;
            }
        }
            private void СhangeAbonent_Click(object sender, RoutedEventArgs e)
                    {
            if (Abonenti.SelectedItem != null)
            {
                // Получить выбранный элемент из DataGrid
                var selectedAbonent = Abonenti.SelectedItem as Абоненты;

                // Обновить свойства выбранного элемента на основе данных, введенных в TextBox
                selectedAbonent.Фамилия = F.Text;
                selectedAbonent.Имя = I.Text;
                selectedAbonent.Отчество = O.Text;
                selectedAbonent.Адрес = Adress.Text;
                selectedAbonent.Номер_телефона = int.Parse(Phonenumber.Text); // Пожалуйста, убедитесь, что вводимое значение может быть преобразовано в int
                selectedAbonent.Города_и_населенные_пункты = (Города_и_населенные_пункты)Vibor_goroda.SelectedItem;

                try
                {
                    // Сохранить изменения в базе данных
                    TelephonesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация об абоненте обновлена");
                    TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    Abonenti.ItemsSource = TelephonesEntities.GetContext().Абоненты.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении информации об абоненте: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите абонента для изменения.");
            }
        }

        private void DeleteAbonent_Click(object sender, RoutedEventArgs e)
        {
            if (Abonenti.SelectedItems != null && Abonenti.SelectedItems.Count > 0)
            {
                var selectedAbonents = Abonenti.SelectedItems.Cast<Абоненты>().ToList();
                try
                {
                    // Удалить выбранных абонентов из контекста базы данных
                    TelephonesEntities.GetContext().Абоненты.RemoveRange(selectedAbonents);
                    TelephonesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Абоненты удалены успешно");
                    TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    // Обновить источник данных для DataGrid
                    Abonenti.ItemsSource = TelephonesEntities.GetContext().Абоненты.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении абонентов: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите абонентов для удаления.");
            }
        }
        private void AddnewSlyg_Click(object sender, RoutedEventArgs e)
        {
            
            if (_slygi.ID == 0)
                TelephonesEntities.GetContext().Служебные_телефоны.Add(_slygi);
            Служебные_телефоны newSlygi = new Служебные_телефоны();
            // Заполняем свойства нового объекта данными из TextBox и ComboBox
            newSlygi.Наименование_предприятия = Nazvanie.Text;
            newSlygi.Адрес_предприятия = Adrespred.Text;
            newSlygi.Отдел = otdel.Text;
            newSlygi.Номер_телефона = int.Parse(nomer.Text); // Пожалуйста, убедитесь, что вводимое значение может быть преобразовано в int
            newSlygi.Города_и_населенные_пункты = (Города_и_населенные_пункты)Vibor_goroda_slygi.SelectedItem;

            try
            {
                // Добавляем новый объект в контекст базы данных
                TelephonesEntities.GetContext().Служебные_телефоны.Add(newSlygi);
                // Сохраняем изменения в базе данных
                TelephonesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                // Обновляем список в DataGrid
                DGridSlyg.ItemsSource = TelephonesEntities.GetContext().Служебные_телефоны.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Slygi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGridSlyg.SelectedItem != null)
            {
                // Получить выбранный элемент из DataGrid
                var selectedslyg = DGridSlyg.SelectedItem as Служебные_телефоны; // Замените YourAbonentClass на тип вашего элемента в коллекции данных

                // Установить свойства выбранного элемента как текст для соответствующих TextBox
                Nazvanie.Text = selectedslyg.Наименование_предприятия;
                Adrespred.Text = selectedslyg.Адрес_предприятия;
                otdel.Text = selectedslyg.Отдел;
                nomer.Text = selectedslyg.Номер_телефона.ToString();

                Vibor_goroda_slygi.SelectedItem = selectedslyg.Города_и_населенные_пункты;
            }
        }
        private void ChangeSlyg_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSlyg.SelectedItem != null)
            {
                var selectedslyg = DGridSlyg.SelectedItem as Служебные_телефоны;

                // Обновить свойства выбранного элемента на основе данных, введенных в TextBox
                selectedslyg.Наименование_предприятия = Nazvanie.Text;
                selectedslyg.Адрес_предприятия = Adrespred.Text;
                selectedslyg.Отдел = otdel.Text;
                selectedslyg.Номер_телефона = int.Parse(nomer.Text); // Пожалуйста, убедитесь, что вводимое значение может быть преобразовано в int
                selectedslyg.Города_и_населенные_пункты = (Города_и_населенные_пункты)Vibor_goroda_slygi.SelectedItem;

                try
                {
                    // Сохранить изменения в базе данных
                    TelephonesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация об абоненте обновлена");
                    TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DGridSlyg.ItemsSource = TelephonesEntities.GetContext().Служебные_телефоны.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении информации об абоненте: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите абонента для изменения.");
            }

        }
        private void DeleteSlyg_Click(object sender, RoutedEventArgs e)
        {
            if (DGridSlyg.SelectedItems != null && DGridSlyg.SelectedItems.Count > 0)
            {
                var selectedslyg = DGridSlyg.SelectedItems.Cast<Служебные_телефоны>().ToList();

                try
                {
                    // Удалить выбранных абонентов из контекста базы данных
                    TelephonesEntities.GetContext().Служебные_телефоны.RemoveRange(selectedslyg);
                    TelephonesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Абоненты удалены успешно");
                    TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    // Обновить источник данных для DataGrid
                    DGridSlyg.ItemsSource = TelephonesEntities.GetContext().Служебные_телефоны.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении абонентов: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите абонентов для удаления.");
            }
        }

        private void AddGorod_Click(object sender, RoutedEventArgs e)
        {
            if (_goroda.ID == 0)
                TelephonesEntities.GetContext().Города_и_населенные_пункты.Add(_goroda);
            Города_и_населенные_пункты newgorod = new Города_и_населенные_пункты();
            // Заполняем свойства нового объекта данными из TextBox и ComboBox
            newgorod.Название_населенного_пункта = Nazvaniep.Text;
            newgorod.Тип_населенного_пункта = Tip.Text;
            

            try
            {
                // Добавляем новый объект в контекст базы данных
                TelephonesEntities.GetContext().Города_и_населенные_пункты.Add(newgorod);
                // Сохраняем изменения в базе данных
                TelephonesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                // Обновляем список в DataGrid
                DgridGoroda.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DgridGoroda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgridGoroda.SelectedItem != null)
            {
                // Получить выбранный элемент из DataGrid
                var selectedgoroda = DgridGoroda.SelectedItem as Города_и_населенные_пункты; // Замените YourAbonentClass на тип вашего элемента в коллекции данных

                // Установить свойства выбранного элемента как текст для соответствующих TextBox
                Nazvaniep.Text = selectedgoroda.Название_населенного_пункта;
                Tip.Text = selectedgoroda.Тип_населенного_пункта;
            }
        }

        private void ChangeGorod_Click(object sender, RoutedEventArgs e)
        {
            if (DgridGoroda.SelectedItem != null)
            {
                var selectedgoroda = DgridGoroda.SelectedItem as Города_и_населенные_пункты;

                // Обновить свойства выбранного элемента на основе данных, введенных в TextBox
                selectedgoroda.Название_населенного_пункта = Nazvaniep.Text;
                selectedgoroda.Тип_населенного_пункта = Tip.Text;


                try
                {
                    // Сохранить изменения в базе данных
                    TelephonesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация об абоненте обновлена");
                    TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    DgridGoroda.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении информации об абоненте: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите абонента для изменения.");
            }
        }

        private void DeleteGorod_Click(object sender, RoutedEventArgs e)
        {
            if (DgridGoroda.SelectedItems != null && DgridGoroda.SelectedItems.Count > 0)
            {
                var selectedgoroda = DgridGoroda.SelectedItems.Cast<Города_и_населенные_пункты>().ToList();

                try
                {
                    // Удалить выбранных абонентов из контекста базы данных
                    TelephonesEntities.GetContext().Города_и_населенные_пункты.RemoveRange(selectedgoroda);
                    TelephonesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Абоненты удалены успешно");
                    TelephonesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    // Обновить источник данных для DataGrid
                    DgridGoroda.ItemsSource = TelephonesEntities.GetContext().Города_и_населенные_пункты.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении абонентов: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите абонентов для удаления.");
            }
        }

        private void OtchetAbonenti_Click(object sender, RoutedEventArgs e)
        {
            AbonentiOtchet abon = new AbonentiOtchet();
            abon.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SlygiOtchet sl = new SlygiOtchet();
            sl.Show();
        }
    }
}
