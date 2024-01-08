
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
   int id, //id og the engineer
   string? fullName = null, //the engineer's full name
   string? mail = null, //the engineer's mail
   double? payPerHour=null, //the engineer's payment per hour
   EngineerExpireance level=EngineerExpireance.Beginner //the engineer's level of expiriance

)
{
    public Engineer() : this(0) { }
}