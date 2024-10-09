using System.Globalization;

namespace ScheduleManager.Data.ViewModels;

public class ScheduleViewModel
{
    public IEnumerable<LessonViewModel> Lessons { get; set; } = [];

    public DayOfWeek Day { get; set; } = DateTime.Today.DayOfWeek;
    public byte WeekDuration => (byte)Enum.GetValues(typeof(DayOfWeek)).Length;

    public byte StartHour { get; set; }
    public byte EndHour { get; set; }

    public static byte GetWeek(DateTime date)
    {
        var culture = CultureInfo.CurrentCulture;
        var calendar = culture.Calendar;
        var dateTimeFormat = culture.DateTimeFormat;

        return (byte)calendar.GetWeekOfYear(date, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
    }
}
