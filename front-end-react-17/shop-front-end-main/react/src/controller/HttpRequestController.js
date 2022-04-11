import axios from "axios";

class HttpRequestController {

    async getCategoriesLeaves() {
        try {
            const categories = await axios.get("/categories/filter/leaves")
            console.log("Cateogires are", categories)
            return categories.data
        } catch (error) {
            return "error"
        }
    }

    registrationPost(newUser) {
        axios.post('/auth/signup', newUser).then(
            res => {
                console.log(res)
            }
        ).catch(
            err => {
                console.log(err);
            }
        )
    }

    async loginPost(data) {
        let user = "";
        try {
            const res = await axios.post('/auth/signin', data)
            console.log(res);
            localStorage.setItem('token', res.data.accessToken);
            //console.log("OK!!!");
            //console.log(localStorage.getItem('token'));
            user = res.data;

        } catch (error) {
            console.log(error)
            user = "bad-credentials";
        }

        return user;
    }

    async addProduct(data) {
        let product = null;
        try {
            const res = await axios.post('/products/add', data)
            console.log(res);
            product = res.data;
        } catch (error) {
            console.log(error);
            product = "not-ok";
        }
        return product;
    }

    async validateProduct(data) {
        let product = null;
        try {
            const res = await axios.post('/products/validate', data)
            console.log(res);
            product = res.data;
        } catch (error) {
            console.log(error);
            product = "not-ok";
        }
        return product;
    }

    async updateProduct(data, code) {
        let product = null;
        try {
            const res = await axios.put('/product/' + code, data)
           // console.log(res);
            product = res.data;
        } catch (error) {
            console.log(error);
            product = "not-ok";
        }
        return product;
    }

    async updateAuction(data, code) {
        let product = null;
        try {
            console.log("am ajuns bine")
            const res = await axios.post('/auction/bid/' + code, data)
            console.log("Raspunsul este", res);
            product = res.data;
        } catch (error) {
            console.log(error);
            product = "not-ok";
        }

        return product;
    }

    

    async deleteImageFromProduct(code, title) {
        let re = null;
        //console.log(title)
        try {
            const res = await axios.post('/photos/delete/' + code + '/' + title)
            console.log(res);
            re = res.data;
        } catch (error) {
            console.log(error);
            re = "not-ok";
        }
        return re;
    }

    logoutPost() {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
        //console.log("post pe logout");
        //console.log(localStorage.getItem('token'));
        axios.get('/auth/signout').then(
            res => {
                console.log(res);
            }
        ).catch(err => {
            console.log(err)
        });
        localStorage.setItem('token', null);
        //console.log(localStorage.getItem('token'));
    }

    async getDefaultThumbnail() {
        try {
            const photo = await axios.get("/default-thumbnail");
            //console.log(photo, "photo is")
            return photo.data;
        } catch (error) {
            console.log(error);
            return "error";
        }
    }


    async validateBid(newBid, code){
        try {
            const res = await axios.get("/product/validate-bid/" + code + "/" + newBid)
            console.log("response", res)
            return res.data;
        } catch(error) {
            console.log(error);
            return "error";
        }
    }
    
    async getProductById(id) {
        //console.log('Controller id is ', id);
        let url = "/product/" + id;
        //console.log(url);
        try {
            const product = await axios.get(url);
            return product.data;
        } catch (error) {
            console.log(error);
            return "error";
        }
    }

    async getCategories() {
        try {
            const categories = await axios.get("/categories")
            //console.log("Request data is", categories)
            return categories.data;
        } catch (error) {
            return "error"
        }
    }

    async getProducts() {
        try {
            const products = await axios.get("/products")
            console.log("Request data is", products)
            return products.data;
        } catch (error) {
            return "error"
        }
    }

    async getFiltersByCategory(category) {
        try {
            let url = "/categories/filter/" + category;
            const categories = await axios.get(url)
            return categories.data;
        } catch (error) {
            return "error"
        }
    }

    async getProductsByCategory(category) {
        try {
            let url = "/product/category/" + category;
            const products = await axios.get(url)
            return products.data;
        } catch (error) {
            return "error"
        }
    }

    async getUser() {
        if (localStorage.getItem('token') != null) {
            try {
                axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
                const response = await axios.get('/users');
                //console.log(response);
                return response.data;
            } catch (error) {
                console.error(error);
            }
        }
        else {
            return null;
        }
    }

    async getAuctionEligibleProducts() {
        try {
            const products = await axios.get("/product/auction-eligible")
            return products.data;

        } catch (error) {
            return "error";
        }
    }

    async shopCart(files) {
        if (files != null) {
            try {
                const products = await axios.post("/shopCart", files)
                return products.data;
            } catch (error) {
                return "error";
            }
        }
    }

    async saveOrder(files) {
        console.log(files)
        if (files != null) {
            try {
                const products = await axios.post("/orders", { id: files.id, address: files.address, payType: files.payType })
                return products.data;
            } catch (error) {
                return "error";
            }
        }
    }

    async saveLang(files) {
        if (files != null) {
            try {
                const products = await axios.post("/users", files)
                return products.data;
            } catch (error) {
                return "error";
            }
        }
    }

    async saveAddress(files) {
        if (files != null) {
            try {
                const products = await axios.post("/users/details", files)
                return products.data;
            } catch (error) {
                return "error";
            }
        }
    }

    async getAddress() {
        try {
            const products = await axios.get("/users/details")
            return products.data;
        } catch (error) {
            return "error";
        }
    }

    async getCurrency() {
        try {
            const currency = await axios.get("https://api.currencyfreaks.com/latest?apikey=dd3a823b95024ea9931e818e3d6506bb");
            console.log("Currency", currency);
            return currency;
        } catch(error) {
            return error;
        }
    }

    async notifyOutbid(product) {
        try {
            console.log("suntem aici");
            const response = await axios.post("/auction/outbid", product);
            console.log(response, "venit de la outbid");
            return response.data;
        } catch(error) {
            return error;
        }
    }

    async checkout(paymentRequest) {
        console.log("PaymentRequest", paymentRequest);
        try {
            const res = await axios.post("/checkout", paymentRequest);
            console.log("Checkout response axios:" + res);
            return res.data;
        } catch(error) {
            return "error";
        }
    }
}

export default new HttpRequestController();
