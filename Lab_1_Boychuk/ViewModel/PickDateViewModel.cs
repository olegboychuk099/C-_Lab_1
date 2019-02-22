using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab_1_Boychuk.Tools;

namespace Lab_1_Boychuk.ViewModel
{
    internal class PickDateViewModel : BaseViewModel
    {
        static readonly string[] HOROSCOPE = {   "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces" };
        static readonly string[] CHINESE_ZODIAC = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
        #region Fields
        private DateTime _date = DateTime.Today;
        private string   _birthday;
        private string   _age;
        private string   _horoscope;
        private string   _chineseZodiac;

        #region Commands
        private RelayCommand <object> _checkDate;
        #endregion
        #endregion

        #region Properties
        public DateTime Date
        {
            get {return _date;}
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get {return _age;}
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get {return _birthday;}
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string Horoscope
        {
            get {return _horoscope; }
            set
            {
                _horoscope = value;
                OnPropertyChanged();
            }
        }

        public string ChineseZodiac
        {
            get{return _chineseZodiac;}
            set
            {
                _chineseZodiac = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        public RelayCommand<object> CheckDate =>
            _checkDate ?? (_checkDate = new RelayCommand<object>(
                CheckDateClick));

        #endregion
        #endregion

        private async void CheckDateClick(object obj)
        {
            ClearFields();
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(2000));
            LoaderManager.Instance.HideLoader();
            if (!IsDateCorrect(Date))
            {
                MessageBox.Show($"Wrong date: {Date.ToShortDateString()}. \nPlease enter the correct date!");
                return;
            }
            Age = "Your age: " + CountAge(Date).ToString();
            Birthday = HasBirthdayToday(Date) ? "I wish you sound health — gain your vital energy from the life itself! \n\tHave good spirits and eternal youth of your heart! \n\t\t\tHappy birthday!" : "Days left until your birthday: " + DayToBirthday(Date).ToString();

            Horoscope = "Horoscope sign: " + HOROSCOPE[Date.Month - 1];
            ChineseZodiac = "Chinese zodiac sign: " + CHINESE_ZODIAC[(Date.Year - 4) % 12];
        }

        

        private int CountAge(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            if (date.Month.CompareTo(DateTime.Today.Month) > 0)
            {
                age--;
            }
            else if (date.Month.CompareTo(DateTime.Today.Month) == 0 && date.Day.CompareTo(DateTime.Today.Day) > 0)
            {
                age--;
            }
            return age;
        }

        private Boolean IsDateCorrect(DateTime date)
        {
            return date.CompareTo(DateTime.Today) <= 0 && DateTime.Today.Year - date.Year < 135;
        }

        private int DayToBirthday(DateTime date)
        {
            
            int days = DateTime.Today.DayOfYear - date.DayOfYear;
            if (days > 0)
            {
                days = DateTime.MaxValue.DayOfYear - DateTime.Today.DayOfYear + date.DayOfYear;
            }
            return Math.Abs(days);
        }

        private Boolean HasBirthdayToday(DateTime date)
        {
            return Date.Month == DateTime.Today.Month && Date.Day == DateTime.Today.Day;
        }

        private void ClearFields()
        {
            Birthday = "";
            Age = "";
            ChineseZodiac = "";
            Horoscope = "";
        }
    }
}
