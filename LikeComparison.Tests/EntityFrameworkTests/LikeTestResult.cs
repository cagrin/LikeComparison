namespace LikeComparison.EntityFrameworkTests
 {
    using System.ComponentModel.DataAnnotations;

    public class LikeTestResult
    {
        [Key]
        public DateTime TestCase { get; set; }

        public bool Comparison { get; set; }
    }
 }