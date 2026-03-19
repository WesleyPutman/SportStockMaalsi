public class EmplacementStock{
    public int Id { get; set; }
    public string Zone { get; set; } = string.Empty;
    public string Allee { get; set; } = string.Empty;
    public string Rayon { get; set; } = string.Empty;
    public string Niveau { get; set; } = string.Empty;

    public int ArticleSportId { get; set; }
    public ArticleSport? Article { get; set; }
}