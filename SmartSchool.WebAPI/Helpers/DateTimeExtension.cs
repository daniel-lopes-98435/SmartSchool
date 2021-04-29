using System;
namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtension
    {
        public static int GetCurrentAge(this DateTime datetime){
       
            var currenteDate = DateTime.UtcNow;
            int age = currenteDate.Year - datetime.Year;
            if(currenteDate < datetime.AddYears(age)){
                age --;
            }

            return age;
        }
    }
}