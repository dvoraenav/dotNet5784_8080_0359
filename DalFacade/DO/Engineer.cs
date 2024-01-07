
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
( int id,
  string? fullName,
  string? mail,
  int level,
  double payPerHour
)
{
    public Engineer() : this(0,null,null,0,0) { }
}