namespace App.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; private set; }


        public Pager()
        {

        }

        public Pager(int totalItems, int currentPage, int pageSize)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((decimal)TotalItems/(decimal)pageSize);
            StartPage = currentPage - 4;
            EndPage = currentPage + 4;
            if(StartPage <= 0)
            {
                EndPage = EndPage - (StartPage - 1);
                StartPage = 1;
            }
            if(EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > 8)
                {
                    StartPage = EndPage - 8;
                }
            }
        }
    }
}
