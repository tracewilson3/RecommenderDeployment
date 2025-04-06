using RecommenderDeployment.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register services BEFORE calling builder.Build()
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(new ContentFilteringSimilarityService("Data/content_filtering.csv"));
builder.Services.AddSingleton(new CollaborativeRecommendationService("Data/sampled_article_recommendations.csv"));

var app = builder.Build();

// ✅ Configure the HTTP pipeline AFTER builder.Build()
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
