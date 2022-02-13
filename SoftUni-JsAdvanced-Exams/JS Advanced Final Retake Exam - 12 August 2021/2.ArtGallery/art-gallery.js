class ArtGallery {
    constructor(creator) {
        this.creator = creator,
            this.possibleArticles = { "picture": 200, "photo": 50, "item": 250 },
            this.listOfArticles = [],
            this.guests = []
    }
 
    addArticle(articleModel, articleName, quantity) {
 
        let articleModelLowerCase = articleModel.toLowerCase();
 
        let quantityNumb = Number(quantity);
        let isExist = false;
 
        for (const key of Object.keys(this.possibleArticles)) {
            if (key === articleModelLowerCase) {
                isExist = true;
                break;
            }
        }
 
        if (!isExist) {
            throw new Error("This article model is not included in this gallery!");
        }
 
        let obj = this.listOfArticles.find(o => o.articleName === articleName);
 
        if (obj) {
 
            obj.quantity += quantityNumb;
 
        } else {
 
            this.listOfArticles.push({ articleModel: articleModelLowerCase, articleName, quantity : quantityNumb});
        }
 
        return `Successfully added article ${articleName} with a new quantity- ${quantity}.`
    }
 
    inviteGuest ( guestName, personality){
        let obj = this.guests.find(o => o.guestName === guestName);
 
        if (obj) {
            return `${guestName} has already been invited.`;
        }
 
        if (personality==='Vip') {
 
            this.guests.push({guestName, points:500, purchaseArticle: 0});
 
        }else if(personality === 'Middle'){
 
            this.guests.push({guestName, points:250, purchaseArticle: 0});
 
        }else{
            this.guests.push({guestName, points:50, purchaseArticle: 0});
        }
 
        return `You have successfully invited ${guestName}!`
    }
 
    buyArticle ( articleModel, articleName, guestName){
 
        let isExist = false;
 
        for (const key of Object.keys(this.possibleArticles)) {
            if (key === articleModel) {
                isExist = true;
                break;
            }
        }
 
        let article = this.listOfArticles.find(o => o.articleName === articleName && o.articleModel === articleModel);
 
        if (!isExist || !article) {
            throw new Error("This article is not found.");
        }
 
        if (article.quantity == 0) {
            return `The ${articleName} is not available.`;
        }
 
        let guest = this.guests.find(o => o.guestName === guestName);
 
        if (!guest) {
 
            return `This guest is not invited.`;
        }
 
        let neededPoints = this.possibleArticles[articleModel];
 
        if (guest.points - neededPoints < 0  ) {
 
            return `You need to more points to purchase the article.`;
 
        }else{
            guest.points-= neededPoints;
            guest.purchaseArticle+=1;
            article.quantity-=1;
        }
 
        return `${guestName} successfully purchased the article worth ${neededPoints} points.`
 
    }
 
    showGalleryInfo (criteria){
 
        let result = '';
 
        if (criteria === 'article') {
 
            result+=  'Articles information:'+ '\n';
 
            for (const key of Object.values(this.listOfArticles) ) {
 
               result += `${key.articleModel} - ${key.articleName} - ${key.quantity}`+ '\n';
            }
        }else{
            result += 'Guests information:'+ '\n';
 
            for (const key of Object.values(this.guests) ) {
                result += `${key.guestName} - ${key.purchaseArticle}`+ '\n';
            }
        }
 
        return result.trim();
    }
}