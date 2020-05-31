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
    public abstract Create(docid:string):Promise<void>;
}

class Menu extends BObject {
    public Code:String = "";
    public UserID:String = "";
    public Foods:Food[] = [];

    public async Create(docid:string): Promise<void> {
        const query = admin.firestore().collection("data_menu").doc(docid);
        const doc = (await query.get()).data()!;

        this.Code = doc.Code;
        this.UserID = doc.UserID;

        for (const ifood of doc.Foods) {
            const food:Food = new Food();
            food.Unit = ifood.Unit;
            food.Quantity = ifood.Quantity;
            
            const code:FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>
            = ifood.Code;

            const data = await code.get();
            if (data.exists) {
                food.Data = new FoodData();

                await food.Data.Create(data.id);

                food.Code = data.data()!.식품코드;
    
                this.Foods.push(food);
            }
        }
    } // END OF Create METHOD
}

class Food {
    public Code:String = "";
    public Quantity:Number = 0;
    public Unit:String = "";

    public Data:FoodData | null = null;
}

class Nut extends BObject {
    public Quantity:number = 0;
    public Unit:String = "";
    public Name:String = "";
    public Code:String = "";

    public async Create(docid: string): Promise<void> {
        const query = admin.firestore().collection("data_nut").doc(docid);
        const doc = (await query.get()).data()!;

        this.Code = docid;
        this.Name = doc.Name;
        this.Unit = doc.Unit;
    }
}

class FoodData extends BObject {
    public 식품코드:String = "";
    public DB군:String = "";
    public 식품명:String = "";
    public 내용량:number = 0;
    public 내용량_단위:String = "";
    public 식품군명:String = "";

    public 영양소:Nut[] = [];

    public async Create(docid:string): Promise<void> {
        const query = admin.firestore().collection("data_foods").doc(docid);
        const doc = (await query.get()).data()!;

        this.식품코드 = doc.식품코드;
        this.DB군 = doc.DB군;
        this.식품명 = doc.식품명;
        this.내용량 = doc.내용량;
        this.내용량_단위 = doc.내용량_단위;
        this.식품군명 = doc.식품군명;

        
        if (doc.영양소 && doc.영양소.length) {
            for (let i:number = 0; i < doc.영양소.length; i++) {
                const code:FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>
                = doc.영양소[i].Code;

                const nut:Nut = new Nut();
                nut.Code = code.id;
                
                const quantity:number = doc.영양소[i].Quantity;
                nut.Quantity = quantity;

                this.영양소.push(nut);
            }
        }
    }
}

class Post extends BObject {
    public Code:String = "";
    public UserID:String = "";
    public Context:String = "";
    public Date:Date | null = null;
    public Menus:Menu[] = [];
    public Comments:Comment[] = [];

    public async Create(docid:string): Promise<void> {
        const query = admin.firestore().collection("data_post").doc(docid);

        const doc = (await query.get()).data()!;

        this.Code = docid;
        this.UserID = doc.p_id;
        const timestamp:admin.firestore.Timestamp = doc.p_date;
        this.Date = timestamp.toDate();
        this.Context = doc.p_text;

        if (doc.p_menu && doc.p_menu.length) {
            for (let i:number = 0; i < doc.p_menu.length; i++) {
                const menudocref:FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>
                = doc.p_menu[i];
    
                const docdata = await menudocref.get();
                if (!docdata.exists) {
                    continue;
                }
                
                const menu:Menu = new Menu();
                await menu.Create(docdata.id);
    
                this.Menus.push(menu);
            }
        }

        const commentcollection = query.collection("data_post_comment");

        await commentcollection.get().then(snapshot => {
            for (let i:number = 0; i < snapshot.size; i++) {
                const cdoc = snapshot.docs[i];

                const comm:Comment = new Comment();
                comm.ID = cdoc.data().c_id;
                comm.Context = cdoc.data().c_text;
                const ctimestamp:admin.firestore.Timestamp = cdoc.data().c_time;
                comm.Date = ctimestamp.toDate();
                comm.Code = cdoc.id;

                this.Comments.push(comm);
            }
        })
        .catch(err => {
            console.error(err);
        });
    }
}

class Comment  {
    public Code:string = "";
    public ID:string = "";
    public Context:string = "";
    public Date:Date | null = null;
}

 export const helloWorld = functions.https.onRequest((request, response) => {
    const text:string = request.body.ID ?? "";
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
    const user:User = new User(request.body.ID, request.body.Password);
    const loginresult:boolean = await user.sign(true);

    const responseresult:any = {};
    responseresult.Packet = "Login";
    responseresult.ID = request.body.ID;
    responseresult.Password = request.body.Password;
    responseresult.Result = false;
    responseresult.Context = "";

    if(loginresult){
        responseresult.Result = true;
        responseresult.Context = JSON.stringify(user);

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
    const ID:string = req.body.ID;
    const PW:string = req.body.Password;

    const user:User = new User(ID, PW);

    const signupresult = await user.sign(false);

    const resresult:any = {};
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
    const user:User = new User(req.body.ID, req.body.Password);
    await user.sign(true);
    
    res.send(user);
});

/**
 * 
 */
export const SetUserData = functions.https.onRequest(async (req, res) => {
    const user:User = new User(req.body.ID, req.body.Password);
    await user.sign(true);

    const height:Number = req.body.Height;
    const weight:Number = req.body.Weight;
    const age:Number = req.body.Age;
    const gender:Boolean = req.body.Gender;
    const name:String = req.body.Name;

    const info:any = {
        "u_height" : height,
        "u_weight" : weight,
        "u_age" : age,
        "u_sex" : gender,
        "u_name" : name 
    };

    const resresult:any = {};
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
    const menu:Menu = new Menu();
    menu.Code = req.body.Code; menu.UserID = req.body.UserID; menu.Foods = req.body.Foods;
    
    const foods:any[] = [];

    // 음식 정보 초기화
    for (let i:number = 0; i < menu.Foods.length; i++) {
        var docref = admin.firestore().doc("data_foods/" + menu.Foods[i].Code.toString());
        var docsnapshot = await docref.get();
        if (!docsnapshot.exists) {
            if (menu.Foods[i].Data != null) {
                const fd:FoodData = menu.Foods[i].Data!;
                const fooddata:any = {};
                fooddata.식품명 = fd.식품명;
                fooddata.DB군 = fd.DB군;
                fooddata.내용량 = fd.내용량;
                fooddata.내용량_단위 = fd.내용량_단위;
                fooddata.식품군명 = fd.식품군명;
                fooddata.식품코드 = fd.식품코드;

                const nuts:any[] = [];

                for (const nut of fd.영양소) {
                    const nutdocref:FirebaseFirestore.DocumentReference
                    = admin.firestore().doc("data_nut/" + nut.Code);
                    nuts.push({
                        "Code" : nutdocref,
                        "Quantity" : nut.Quantity
                    });
                }

                fooddata.영양소 = nuts;

                await docref.set(fooddata);
            }
        }

        foods.push({
            "Code" : admin.firestore().doc("data_foods/" + menu.Foods[i].Code.toString()),
            "Quantity" : menu.Foods[i].Quantity,
            "Unit" : menu.Foods[i].Unit
        });
    }

    // 식단 정보 초기화
    const menuj:any = {
        "Code" : menu.Code,
        "UserID" : menu.UserID,
        "Foods" : foods
    };

    // 패킷 데이터 초기화
    const resresult:any = {};
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
    const ID:String = req.body.ID ?? "";
    //let Password:String = req.body.Password;

    const query = admin.firestore().collection("data_menu").where("UserID", "==", ID);

    // 클라이언트에 보낼 메뉴 정보
    const menus:Menu[] = [];
    let menucode:String = "";

    await query.get().then(async (snapshot) => {
        for (let index:number = 0; index < snapshot.size; index++) {
            const doc = snapshot.docs[index];

            if (doc.data().UserID != ID) continue;

            const menu:Menu = new Menu();
            await menu.Create(doc.id);

            menucode = menucode + "" + menu.Code + "\n";

            menus.push(menu);
        }
    }).catch(err => console.error(err));

    console.log(menucode);

    res.send(menus);
});

export const DeleteMenuData = functions.https.onRequest(async (req, res) => {
    const Code:String = req.body.Code;
    
    const collection = admin.firestore().collection("data_menu");
    const doc = collection.doc(Code.toString());
    doc.delete().catch(err => console.error(err));

    const resresult:any = {};
    resresult.Packet = "DeleteMenuData";
    resresult.Result = true;
    resresult.Context = "";

    res.send(resresult);
});

export const SearchFood = functions.https.onRequest(async (req, res) => {
    const Search:String = req.body.Search;

    const foods:any[] = [];

    const query = admin.firestore().collection("data_foods").orderBy("식품명").startAt(Search).endAt(Search + "\uF8FF"); 
    await query.get().then((snapshot) => {
        for (let i:number = 0; i < snapshot.size; i++) {
            const doc = snapshot.docs[i];

            foods.push(doc.data().식품명);         
        }
    }).catch(err => {
        console.error(err);
    });
    
    res.send(foods);
});

/**
 * 공유 식단 게시글 작성
 */
export const SetPostData = functions.https.onRequest(async (req, res) => {
    const ID:String = req.body.UserID ?? "";
    const DateTime:number = req.body.DateTime ?? 0;
    const Context:String = req.body.Context ?? "";
    const MenuCode:String[] = req.body.Menu ?? [];

    const timestamp:admin.firestore.Timestamp = admin.firestore.Timestamp.fromMillis(DateTime);
    console.log(DateTime);
    console.log(timestamp);

    const post:any = {
        "p_id" : ID,
        "p_date" : admin.firestore.Timestamp.fromMillis(DateTime),
        "p_text" : Context
    };
    
    const menus:any[] = [];
    for (let i:number = 0; i < MenuCode.length; i++) {
        menus.push(admin.firestore().doc("data_menu/" + MenuCode[i]));
    }
    post.p_menu = menus;

    // 패킷 데이터 초기화
    const resresult:any = {};
    resresult.Packet = "SetPostData";
    resresult.Result = true;
    resresult.Context = "";
    
    const data:void | FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData> =
    await admin.firestore().collection("data_post")
    .add(post)
    .catch(err => {
        console.error(err);
        resresult.Result = false;
        resresult.Context = "식단 등록에 실패하였습니다.";
    });

    if ((<FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>>data).id) {
        resresult.Context = (<FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>>data).id;
    }

    res.send(resresult);
});


/**
 * 공유 식단 게시글 정보 
 */
export const GetPostData = functions.https.onRequest(async (req, res) => {
    const query = admin.firestore().collection("data_post");

    const resresult:any[] = [];

    await query.get().then(async (snapshot) => {
        for (let i:number = 0; i < snapshot.size; i++) {
            const doc = snapshot.docs[i];

            const post:Post = new Post();
            await post.Create(doc.id);

            resresult.push(post);
        }
    }).catch(err => {
        console.error(err);
    });

    res.send(resresult);
});

/**
 * 공유 식단 게시글 댓글 작성
 */
export const AddCommentData = functions.https.onRequest(async (req, res) => {
    const ID:String = req.body.ID ?? "";
    const PostID:String = req.body.PostID ?? "";
    const DateTime:number = req.body.DateTime ?? 0;
    const Context:String = req.body.Context ?? "";

    const time:admin.firestore.Timestamp = admin.firestore.Timestamp.fromMillis(DateTime);

    const query = admin.firestore().collection("data_post").doc(PostID.toString()).collection("data_post_comment");

    const resresult:any = {};
    resresult.Packet = "AddCommentData";
    resresult.Result = true;
    resresult.Context = "";

    const data:void | FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData> =
    await query.add({
        "c_id" : ID,
        "c_text" : Context,
        "c_time" : time
    }).catch(err => {
        console.error(err);
        resresult.Result = false;
    });

    if ((<FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>>data).id) {
        resresult.Context = (<FirebaseFirestore.DocumentReference<FirebaseFirestore.DocumentData>>data).id;
    }

    res.send(resresult);
});