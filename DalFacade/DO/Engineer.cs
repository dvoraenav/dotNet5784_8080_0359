﻿
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="id">Personal unique ID of the Engineer</param>
/// <param name="fullName"></param>
/// <param name="mail"></param>
/// <param name="level"></param>
/// <param name="payPerHour"></param>
public record Engineer
(
   int Id, //id of the engineer
   string FullName, //the engineer's full name
   string Mail, //the engineer's mail
   double Cost, //the engineer's payment per hour
   EngineerExpireance Level = EngineerExpireance.Beginner //the engineer's level of expiriance

)
{
    public Engineer() : this(0, "", "", 0, EngineerExpireance.Beginner) { }
}