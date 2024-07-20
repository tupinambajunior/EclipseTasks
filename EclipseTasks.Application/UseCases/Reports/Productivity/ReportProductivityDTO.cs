namespace EclipseTasks.Application.UseCases.Reports.Productivity
{
    public class ReportProductivityDTO
    {
        public IEnumerable<DataDTO> Data { get; set; }

        public class DataDTO
        {
            public decimal Average { get; set; }
            public string UserEmail { get; set; }
        }
    }
}