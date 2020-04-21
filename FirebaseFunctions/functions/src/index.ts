// // Start writing Firebase Functions
// // https://firebase.google.com/docs/functions/typescript
//

import * as functions from 'firebase-functions';
import { firebaseConfig } from 'firebase-functions';
import * as admin from 'firebase-admin';

var serviceAccount = require("path/to/serviceAccountKey.json");

admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
  databaseURL: "https://babzoom-7cae1.firebaseio.com"
});

 export const helloWorld = functions.https.onRequest((request, response) => {
    var text:string = request.body.ID ?? "";
    response.send({ "a" : text });
 });

class User {
    userId:String;
    userPw:String;

    constructor(id:String, pw:String){
        this.userId = id;
        this.userPw = pw;
    }
}

function login(user : User):boolean {
     if(user.userId == "Yuseong" && user.userPw == "Choi"){
        return true;
     }
     return false;
}

export const example = functions.https.onRequest((request, response) => {
    const a = new User(request.body.ID, request.body.Password);
    if(login(a)){
        response.send({"ID" : request.body.ID, "Password" : request.body.Password, "context" : "아이디,비밀번호가 일치합니다."});
    } else {
        response.send({"ID" : request.body.ID, "Password" : request.body.Password, "context" : "아이디,비밀번호를 다시 확인하세요."});
    }
});