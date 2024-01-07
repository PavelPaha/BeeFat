using BeeFat.Data;

namespace BeeFat;

public static class MetabolismCalculator
{
    public static int CalculateBMRForFemaleWithoutActivity(double weight, double height, int age)
    {
        var bmr = 9.99 * weight + 6.25 * height - 4.92 * age - 161;
        return (int)bmr;
    }
        
    public static int CalculateBMRForMaleWithoutActivity(double weight, double height, int age)
    {
        var bmr = 9.99 * weight + 6.25 * height - 4.92 * age + 5;
        return (int)bmr;
    }
        
    public static int CalculateBMRForFemaleWithActivity(double weight, double height, int age, double activityLevel)
    {
        var bmrWithoutActivity = CalculateBMRForFemaleWithoutActivity(weight, height, age);
        var bmrWithActivity = bmrWithoutActivity * activityLevel;
        return (int)bmrWithActivity;
    }
        
    public static int CalculateBMRForMaleWithActivity(double weight, double height, int age, double activityLevel)
    {
        var bmrWithoutActivity = CalculateBMRForMaleWithoutActivity(weight, height, age);
        var bmrWithActivity = bmrWithoutActivity * activityLevel;
        return (int)bmrWithActivity;
    }
    
    // 1,2 — для малоподвижных людей;
    // 1,375 — низкая активность;
    // 1,550 — умеренная активность;
    // 1,725 — высокая активность;
    // 1,900 — очень высокая активность.
    
    public static int CalculateMetabolism(ApplicationUser user)
    {
        if (user.Gender == Gender.Female)
            return
                CalculateBMRForFemaleWithActivity(user.Weight, user.Height, user.Age,
                    user.ActivityLevel);
        return CalculateBMRForMaleWithActivity(user.Weight, user.Height, user.Age,
            user.ActivityLevel);
    }
}