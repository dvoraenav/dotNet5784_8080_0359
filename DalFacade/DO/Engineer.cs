
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
   int id=0,
   string? fullName = null,
   string? mail = null,
   double payPerHour=0,
   EngineerExpireance level=EngineerExpireance.Beginner

)
{
    public Engineer() : this(0) { }
}