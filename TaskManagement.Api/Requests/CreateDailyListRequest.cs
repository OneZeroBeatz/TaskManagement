﻿namespace TaskManagement.Api.Requests
{
    public class CreateDailyListRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}