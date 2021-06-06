//export class StoreCustomer {
class StoreCustomer {
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 0;
    }
    showName() {
        alert(this.name);
    }
    set name(val) {
        this.ourName = val;
    }
    get name() {
        return this.firstName + this.lastName;
    }
}
//# sourceMappingURL=storecustomer.js.map