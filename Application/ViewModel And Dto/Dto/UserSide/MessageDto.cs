
using Doamin.Entities.UserEntities;

namespace Application.ViewModel_And_Dto.Dto.UserSide;

public record MessageDto
{


    public int Id { get; set; }
    public int ConverstationId { get; set; }
    public int SenderId { get; set; }
    public string? SenderName { get; set; }
    public int ResiverId { get; set; }
    public string? ResiverName { get; set; }
    public string Content { get; set; }
    public DateTime? Timestamp { get; set; }


}
