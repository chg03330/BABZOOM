// // Start writing Firebase Functions
// // https://firebase.google.com/docs/functions/typescript
//

import * as functions from 'firebase-functions';
//import { firebaseConfig } from 'firebase-functions';
import * as admin from 'firebase-admin';

admin.initializeApp();

//let serviceAccount = require("C:/Users/qweas/Downloads/babzoom-7cae1-firebase-adminsdk-z5h82-3eb4300e68.json");

//admin.initializeApp({
 // credential: admin.credential.cert(serviceAccount),
 // databaseURL: "https://babzoom-7cae1.firebaseio.com"
//});

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

        await admin.firestore().collection('data_user').add({
            "u_id" : ID,
            "u_pw" : PW
        }).catch((err) => console.error(err));
    }
    else {
        resresult.Result = false;
        resresult.Context = "실패";
    }

    res.send(resresult);
})

export const GetUserData = functions.https.onRequest(async (req, res) => {
    let user:User = new User(req.body.ID, req.body.Password);
    await user.sign(true);
    
    res.send(user);
});

export const SetUserData = functions.https.onRequest(async (req, res) => {
    let user:User = new User(req.body.ID, req.body.Password);
    await user.sign(true);

    let height:Number = req.body.Height;
    let weight:Number = req.body.Weight;
    let age:Number = req.body.Age;
    let gender:Boolean = req.body.Gender;
    let name:String = req.body.Name;
    
    await admin.firestore().collection('data_user').add({
        "u_height" : height,
        "u_weight" : weight,
        "u_age" : age,
        "u_sex" : gender,
        "u_name" : name 
    }).catch(err => console.error(err));

    res.send();
});