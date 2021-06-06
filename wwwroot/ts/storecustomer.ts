//export class StoreCustomer {
class StoreCustomer {
    constructor(private firstName:string, private lastName:string) {

    }

    public visits: number = 0;
    public ourName: string;

    public showName() {
        alert( this.name);
    }

    set name (val: string) {
        this.ourName = val;
    }

    get name() {
        return this.firstName + this.lastName;
    }

}