
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
   int Id, //id og the engineer
   string? FullName = null, //the engineer's full name
   string? Mail = null, //the engineer's mail
   double? PayPerHour=null, //the engineer's payment per hour
   EngineerExpireance Level=EngineerExpireance.Beginner //the engineer's level of expiriance

)
{
    public Engineer() : this(0) { }
}