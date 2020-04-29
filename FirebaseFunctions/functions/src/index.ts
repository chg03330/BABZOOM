// // Start writing Firebase Functions
// // https://firebase.google.com/docs/functions/typescript
//

import * as functions from 'firebase-functions';
//import { firebaseConfig } from 'firebase-functions';
import * as admin from 'firebase-admin';

//import * as BasicObject from "BasicObject";

//import * as express from "express";

admin.initializeApp();

//let serviceAccount = require("C:/Users/qweas/Downloads/babzoom-7cae1-firebase-adminsdk-z5h82-3eb4300e68.json");

//admin.initializeApp({
 // credential: admin.credential.cert(serviceAccount),
 // databaseURL: "https://babzoom-7cae1.firebaseio.com"
//});

abstract class BObject {
    public abstract Create(doc:FirebaseFirestore.DocumentData):void;
}

class Menu extends BObject {
    public Code:String = "";
    public UserID:String = "";
    public Foods:Food[] = [];

    public async Create(doc: FirebaseFirestore.DocumentData): Promise<void> {
        this.Code = doc.Code;
        this.UserID = doc.UserID;

        for (let i:number = 0; i < doc.Foods.length; i++) {
            let food:Food = new Food();
            food.Unit = doc.Foods[i].Unit;
            food.Quantity = doc.Foods[i].Quantity;
            
            let code:FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>
            = doc.Foods[i].Code;

            let data = await code.get();
            
            food.Data = new FoodData();
            food.Data.Create(data.data()!);

            food.Code = data.data()!.식품코드;

            this.Foods.push(food);
        }
    }
}

class Food {
    public Code:String = "";
    public Quantity:Number = 0;
    public Unit:String = "";

    public Data:FoodData | null = null;
}

class FoodData extends BObject {
    public 식품코드:String = "";
    public DB군:String = "";
    public 식품이름:String = "";

    public Create(doc:FirebaseFirestore.DocumentData): void {
        this.식품코드 = doc.식품코드;
        this.DB군 = doc.DB군;
        this.식품이름 = doc.식품이름;
    }
}

class Post extends BObject {
    public UserID:String = "";
    public Context:String = "";
    public Date:Date | null = null;
    public Menus:Menu[] = [];

    public async Create(doc: FirebaseFirestore.DocumentData): Promise<void> {
        this.UserID = doc.p_id;
        this.Date = doc.p_date;
        this.Context = doc.p_text;

        for (let i:number = 0; i < doc.p_menu.length; i++) {
            let menudocref:FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>
            = doc.p_menu[i];

            let docdata = await menudocref.get();
            
            let menu:Menu = new Menu();
            await menu.Create(docdata.data()!);

            this.Menus.push(menu);
        }
    }
}

 export const helloWorld = functions.https.onRequest((request, response) => {
    let text:string = request.body.ID ?? "";
    response.send({ "a" : text });
 });

class User {
    userId:String;
    userPw:String;
    age:Number;
    height:Number;
    weight:Number;
    name:String;
    gender:boolean;

    constructor(id:String, pw:String){
        this.userId = id;
        this.userPw = pw;
        this.age = -1;
        this.height = -1;
        this.weight = -1;
        this.name = "";
        this.gender = false;
    }

    async sign(mode:boolean):Promise<boolean> {
        let result:boolean = false;
    
        let query = admin.firestore().collection('data_user').where("u_id", "==", this.userId);
        if (mode) query = query.where("u_pw", "==", this.userPw);

        await query.get().then((snapshot) => {
            if (snapshot.empty) {
                result = false;
            }
            else {
                result = true;
                if (mode) {
                    snapshot.forEach((doc) => {
                        this.userId = doc.data().u_id ?? "";
                        this.userPw = doc.data().u_pw ?? "";
                        this.name = doc.data().u_name ?? "";
                        this.age = doc.data().u_age ?? -1;
                        this.height = doc.data().u_height ?? -1;
                        this.weight = doc.data().u_weight ?? -1;
                        this.gender = doc.data().u_sex ?? false;
                    });
                }
            }
        }).catch((err) => {
            console.error(err);
            result = false;
        });
    
        return result;
    }
}

// 주석 스타일 - JSDoc

/**
* 로그인 기능을 구현하는 함수입니다.
* @class
* @param ID - 로그인 했을 때의 아이디
* @param Password - 로그인 했을 때의 패스워드
* @returns
    Packet - 패킷 종류,
    ID - 로그인 아이디,
    Password - 로그인 패스워드,
    Result - 로그인 성공/실패,
    Context - 안내 텍스트
*/
export const SignIn = functions.https.onRequest(async (request, response) => {
    let a:User = new User(request.body.ID, request.body.Password);
    let loginresult:boolean = await a.sign(true);

    let responseresult:any = {};
    responseresult.Packet = "Login";
    responseresult.ID = request.body.ID;
    responseresult.Password = request.body.Password;
    responseresult.Result = false;
    responseresult.Context = "";

    if(loginresult){
        responseresult.Result = true;
        responseresult.Context = "아이디,비밀번호가 일치합니다.";

        //user = a;
    }
    else {
        responseresult.Result = false;
        responseresult.Context = "아이디,비밀번호를 다시 확인하세요.";
    }

    response.send(responseresult);
});


/**
 * 회원가입 기능을 구현하는 함수입니다.
 * @param ID
 * @param Password
 */
export const SignUp = functions.https.onRequest(async (req, res) => {
    let ID:string = req.body.ID;
    let PW:string = req.body.Password;

    let user:User = new User(ID, PW);

    let signupresult = await user.sign(false);

    let resresult:any = {};
    resresult.Packet = "SignUp";
    resresult.ID = ID;
    resresult.Password = PW;
    resresult.Result = false;
    resresult.Context = "";

    if (!signupresult) {
        resresult.Result = true;
        resresult.Context = "성공";

        await admin.firestore().collection('data_user').doc(ID).set({
            "u_id" : ID,
            "u_pw" : PW
        }).catch((err) => console.error(err));
    }
    else {
        resresult.Result = false;
        resresult.Context = "실패";
    }

    res.send(resresult);
});

/**
 * 
 */
export const GetUserData = functions.https.onRequest(async (req, res) => {
    let user:User = new User(req.body.ID, req.body.Password);
    await user.sign(true);
    
    res.send(user);
});

/**
 * 
 */
export const SetUserData = functions.https.onRequest(async (req, res) => {
    let user:User = new User(req.body.ID, req.body.Password);
    await user.sign(true);

    let height:Number = req.body.Height;
    let weight:Number = req.body.Weight;
    let age:Number = req.body.Age;
    let gender:Boolean = req.body.Gender;
    let name:String = req.body.Name;

    let info:any = {
        "u_height" : height,
        "u_weight" : weight,
        "u_age" : age,
        "u_sex" : gender,
        "u_name" : name 
    };

    let resresult:any = {};
    resresult.Packet = "SetUserData";
    resresult.Result = true;
    resresult.Context = "";
    
    await admin.firestore().collection('data_user')
    .doc(user.userId.toString()).update(info)
    .catch(err => {
        console.error(err);
        resresult.Result = false;
    });

    res.send(resresult);
});

/**
 * 클라이언트에서 보내온 식단 데이터를 서버에 저장합니다.
 * @param Code - 식단을 작성한 일시
 * @param UserID - 식단을 작성한 유저
 * @param Foods - 음식 정보
 * @param Foods.Code - 음식 코드
 * @param Foods.Quantity - 음식 양
 * @param Foods.Unit - 음식 양 단위
 */
export const SetMenuData = functions.https.onRequest(async (req, res) => { 
    //let autoID = admin.firestore().collection("data_menu").doc().id;
    let menu:Menu = new Menu();
    menu.Code = req.body.Code; menu.UserID = req.body.UserID; menu.Foods = req.body.Foods;
    
    let foods:any[] = [];

    // 음식 정보 초기화
    for (let i:number = 0; i < menu.Foods.length; i++) {
        foods.push({
            "Code" : admin.firestore().doc("data_foods/" + menu.Foods[i].Code.toString()),
            "Quantity" : menu.Foods[i].Quantity,
            "Unit" : menu.Foods[i].Unit
        });
    }

    // 식단 정보 초기화
    let menuj:any = {
        "Code" : menu.Code,
        "UserID" : menu.UserID,
        "Foods" : foods
    };

    // 패킷 데이터 초기화
    let resresult:any = {};
    resresult.Packet = "SetMenuData";
    resresult.Result = true;
    resresult.Context = "";

    // 데이터베이스 저장
    await admin.firestore().collection("data_menu").doc(menu.Code.toString())
    .set(menuj)
    .catch(err => {
        console.error(err);
        resresult.Result = false;
        resresult.Context = "식단 등록에 실패 하였습니다.";
    });

    res.send(resresult);
});

/**
 * ID 로 작성한 식단 데이터를 취득합니다.
 * @param ID - 식단을 취득하고 싶은 사용자 아이디
 */
export const GetMenuData = functions.https.onRequest(async (req, res) => {
    let ID:String = req.body.ID;
    //let Password:String = req.body.Password;

    let query = admin.firestore().collection("data_menu").where("UserID", "==", ID);

    // 클라이언트에 보낼 메뉴 정보
    let menus:any[] = [];

    await query.get().then(async (snapshot) => {
        for (let index:number = 0; index < snapshot.size; index++) {
            let doc = snapshot.docs[index];

            // 식단 정보 초기화
            let menu:any = {};
                menu.Code = doc.data().Code;
                menu.UserID = doc.data().UserID;
    
            let foods:any[] = [];
    
            for (let i:number = 0; i < doc.data().Foods.length; i++) {
                // 음식 정보 firestore document
                let foodref:FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>
                = doc.data().Foods[i].Code;
                
                let fooddata = await foodref.get();
    
                // 음식 정보 초기화
                foods.push({
                    "Quantity" : doc.data().Foods[i].Quantity,
                    "Unit" : doc.data().Foods[i].Unit,
                    "Data" : fooddata.data()
                });
            }
            menu.Foods = foods;
    
            menus.push(menu);
        }
    }).catch(err => console.error(err));

    res.send(menus);
});

export const SearchFood = functions.https.onRequest(async (req, res) => {
    let Search:String = req.body.Search;

    let foods:any[] = [];

    let query = admin.firestore().collection("data_foods").orderBy("식품명").startAt(Search).endAt(Search + "\uF8FF"); 
    await query.get().then((snapshot) => {
        for (let i:number = 0; i < snapshot.size; i++) {
            let doc = snapshot.docs[i];

            foods.push(doc.data());         
        }
    }).catch(err => {
        console.error(err);
    });
    
    res.send(foods);
});

/**
 * 
 */
export const SetPostData = functions.https.onRequest(async (req, res) => {
    let ID:String = req.body.UserID ?? "";
    let DateTime:number = req.body.DateTime ?? 0;
    let Context:String = req.body.Context ?? "";
    let MenuCode:String[] = req.body.Menus ?? [];

    let timestamp:admin.firestore.Timestamp = admin.firestore.Timestamp.fromMillis(DateTime);
    console.log(DateTime);
    console.log(timestamp);

    let post:any = {
        "p_id" : ID,
        "p_date" : admin.firestore.Timestamp.fromMillis(DateTime),
        "p_text" : Context
    };
    
    let menus:any[] = [];
    for (let i:number = 0; i < MenuCode.length; i++) {
        menus.push(admin.firestore().doc("data_menu/" + MenuCode[i]));
    }
    post.p_menu = menus;

    // 패킷 데이터 초기화
    let resresult:any = {};
    resresult.Packet = "SetPostData";
    resresult.Result = true;
    resresult.Context = "";
    
    await admin.firestore().collection("data_post")
    .add(post)
    .catch(err => {
        console.error(err);
        resresult.Result = false;
        resresult.Context = "식단 등록에 실패하였습니다.";
    });

    res.send(resresult);
});

export const GetPostData = functions.https.onRequest(async (req, res) => {
    let query = admin.firestore().collection("data_post");

    let resresult:any[] = [];

    await query.get().then(async (snapshot) => {
        for (let i:number = 0; i < snapshot.size; i++) {
            let doc = snapshot.docs[i];

            let post:Post = new Post();
            await post.Create(doc.data());

            resresult.push(post);
        }
    }).catch(err => {
        console.error(err);
    });

    res.send(resresult);
});