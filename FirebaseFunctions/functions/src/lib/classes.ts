declare module "BasicObject" {
    export class Menu {
        public Code:Number;
        public UserID:String;
        public Foods:Food[];
    }

    export class Food {
        public Code:Number;
        public Quantity:Number;
        public Unit:String;
    }
}