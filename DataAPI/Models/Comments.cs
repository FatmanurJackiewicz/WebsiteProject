﻿namespace DataAPI.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Context { get; set; }
        public DateTime CreatedTime { get; set; }
        public byte IsApproved { get; set; }
        public int BlogPostId { get; set; }
        public int UserId { get; set; }


    }
}
