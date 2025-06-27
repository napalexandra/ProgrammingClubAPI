namespace ProgrammingClubAPI.Models
{
    //e o clasa generica -> insemna ca poate lucra cu orice tip de obiecte(Members, CodeSnippets, etc.)
    public class PagedResultDto<T>
    {
        //lista de obiecte returnate de orice tip
        public IEnumerable<T> Items { get; set; } = new List<T>();

        //numarul total de obiecte din tabelul respectiv
        public int TotalCount { get; set; }
    }
}
