using System;
/// <summary>
/// Convertions from full time and part time wages to Salary
/// </summary>
public static class Salary
{
    #region Part Time
    /// <summary>
    /// Calculates total income of part time work
    /// </summary>
    /// <param name="HourlyWage">Standard non overtime wage</param>
    /// <returns>Total income</returns>
    public static double PartTimeGross(double HourlyWage)
	{
		return PartTimeGross(HourlyWage, 24);
	}
    /// <summary>
    /// Calculates total income of part time work
    /// </summary>
    /// <param name="HourlyWage">Standard non overtime wage</param>
    /// <param name="WeeklyHours">hours worked excluding breaks</param>
    /// <param name="OvertimeHours">hours of overtime recived per week</param>
    /// <param name="OvertimeWage">Amount paid per hour of overtime</param>
    /// <returns>Total yearly income</returns>
    public static double PartTimeGross(double HourlyWage, double WeeklyHours)
	{
		double TotalWeekly = HourlyWage * WeeklyHours;
		return TotalWeekly * 52;
	}
    #endregion
    #region Full Time
    /// <summary>
    /// Estimates salary from full time job
    /// </summary>
    /// <param name="HourlyWage">Money made per hour</param>
    /// <returns>The salary made</returns>
    public static double FullTimeGross(double HourlyWage)
    {
        return FullTimeGross(HourlyWage, 40, HourlyWage*1.5, 40);
    }
    /// <summary>
    /// Estimates salary from full time job
    /// </summary>
    /// <param name="HourlyWage">Money made per hour</param>
    /// <param name="WeeklyHours">Hours given per week</param>
    /// <returns>The salary made</returns>
    public static double FullTimeGross(double HourlyWage, double WeeklyHours)
    {
        return FullTimeGross(HourlyWage, WeeklyHours, HourlyWage*1.5, 40);
    }
    /// <summary>
    /// Estimates salary from full time job
    /// </summary>
    /// <param name="HourlyWage">Money made per hour</param>
    /// <param name="WeeklyHours">Hours given per week</param>
    /// <param name="OvertimeWage">amount paid during overtime</param>
    /// <returns>The salary made</returns>
    public static double FullTimeGross(double HourlyWage, double WeeklyHours, double OvertimeWage)
    {
        return FullTimeGross(HourlyWage, WeeklyHours, OvertimeWage, 40);
    }
    /// <summary>
    /// Estimates salary from full time job
    /// </summary>
    /// <param name="HourlyWage">Money made per hour</param>
    /// <param name="WeeklyHours">Hours given per week</param>
    /// <param name="OvertimeWage">amount paid during overtime</param>
    /// <param name="OvertimeLimit">hours until overtime starts</param>
    /// <returns>The salary made</returns>
    public static double FullTimeGross(double HourlyWage, double WeeklyHours, double OvertimeWage, double OvertimeLimit)
    {
        double Overtime;
        if (WeeklyHours > OvertimeLimit)
        {
            Overtime = WeeklyHours - OvertimeLimit;
            WeeklyHours = OvertimeLimit;
        }
        else
        {
            Overtime = 0;
        }
        double WagePay = HourlyWage * WeeklyHours;
        double Overtimepay = OvertimeWage * Overtime;
        return (WagePay + Overtimepay) * 52;
    }
    #endregion
}
