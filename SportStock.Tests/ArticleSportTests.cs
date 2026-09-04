using SportStock.Api.Models;

namespace SportStock.Tests;

public class ArticleSportTests{
	[Theory]
	[InlineData(-100)]
	[InlineData(0)]
	public void AjouterStock_QuantiteInvalide_LeveUneException(int quantiteInvalide){
		var article = new VetementSport("t",1m,"t","t","t","t");
		Assert.Throws<ArgumentOutOfRangeException>(() => article.AjouterStock(quantiteInvalide));
	}
	[Fact]
	public void AjouterStock_QuantiteValide_AugmenteLeStock(){
		var article = new VetementSport("t",1m,"t","t","t","t");
		article.AjouterStock(50);
		Assert.Equal(50, article.QuantiteEnStock);
	}
}