<template>
  <div class="addproperty">
    <p v-if="!isLoggedIn || !isAdmin">
      Please Log in or Register as a Property Owner to Add Properties
    </p>
    <h4 v-if="isLoggedIn && isAdmin" class="text-center mt-3">Add Property</h4>
    <form v-if="isLoggedIn && isAdmin" v-on:submit.prevent="addNewProperty()">
      <div class="mb-3">
        <label for="street" class="form-label"
          >Please Enter Street Address</label
        >
        <input
          type="text"
          class="form-control"
          id="street"
          v-model.trim="newProperty.Street"
          required
          placeholder="Enter address"
        />
      </div>
      <div class="mb-3">
        <label for="city" class="form-label">Please Enter City</label>
        <input
          type="text"
          class="form-control"
          id="city"
          v-model.trim="newProperty.City"
          required
          placeholder="Enter City"
        />
      </div>
      <div class="mb-3">
        <label for="state" class="form-label">Please Enter State</label>
        <input
          type="text"
          class="form-control"
          id="state"
          v-model.trim="newProperty.State"
          required
          placeholder="Enter State"
        />
      </div>
      <div class="mb-3">
        <label for="zipcode" class="form-label">Please Enter ZipCode</label>
        <input
          type="text"
          class="form-control"
          id="zipcode"
          v-model.trim="newProperty.Zipcode"
          required
          placeholder="Enter Zipcode"
        />
      </div>
      <div class="form-group">
        <label for="type">Property Type</label>
        <select
          class="form-control"
          required
          id="type"
          v-model="newProperty.Type"
        >
          <option value="House">House</option>
          <option value="Apartment">Apartment</option>
          <option value="Bussiness">Business</option>
        </select>
      </div>
      <div class="form-group">
        <label for="description"
          >Please give a detailed description of your property</label
        >
        <textarea
          v-model.trim="newProperty.Description"
          class="form-control"
          id="description"
          rows="3"
        ></textarea>
      </div>
      <div class="mb-3">
        <label for="name" class="form-label"
          >Please Enter A Name or Title For Your Property</label
        >
        <input
          type="text"
          class="form-control"
          id="name"
          v-model.trim="newProperty.Name"
          required
          placeholder="Enter Property Name"
        />
      </div>
      <div class="mb-3">
        <label for="beds" class="form-label">Number of Beds</label>
        <input
          type="text"
          class="form-control"
          id="beds"
          v-model.number="newProperty.Beds"
          required
          placeholder="Enter Number of Beds"
        />
      </div>
      <div class="mb-3">
        <label for="baths" class="form-label">Number of Baths</label>
        <input
          type="text"
          class="form-control"
          id="baths"
          v-model.number="newProperty.Baths"
          required
          placeholder="Enter Number of Baths"
        />
      </div>
      <div class="mb-3">
        <label for="rent" class="form-label">Monthly Rent</label>
        <input
          type="text"
          class="form-control"
          id="rent"
          v-model.number="newProperty.Rent"
          required
          placeholder="Enter Monthly Rent"
        />
      </div>
      <div class="mb-3">
        <label for="size" class="form-label">Please Enter Size (in sqft)</label>
        <input
          type="text"
          class="form-control"
          id="size"
          v-model.number="newProperty.Size"
          required
          placeholder="Enter Size of Property in Sqft"
        />
      </div>
      <div>
        Is the Property Available to Rent?
        <input type="radio" :value="true" v-model="newProperty.isAvailable" />
        Yes
        <input type="radio" :value="false" v-model="newProperty.isAvailable" />
        No
      </div>
      <button type="submit" class="btn btn-success">Submit</button>
    </form>
  </div>
</template>



<script>
import PropertyService from "../services/PropertyService.js";

export default {
  data() {
    return {
      newProperty: {
        Street: "",
        City: "",
        State: "",
        Zipcode: "",
        Type: "",
        Description: "",
        Name: "",
        Beds: "",
        Baths: "",
        Rent: "",
        Size: "",
        isAvailable: false,
      },
      showError: false,
      errorMessage: "",
    };
  },
  methods: {
    addNewProperty() {
      PropertyService.addProperty(this.newProperty)
        .then((response) => {
          const newItem = response.data;
          console.log(newItem);

          this.newProperty = {
            Street: "",
            City: "",
            State: "",
            Zipcode: "",
            Type: "",
            Description: "",
            Name: "",
            Rent: "",
            Beds: "",
            Baths: "",
            Size: "",
            isAvailable: false,
          };
          this.$router.push({ name: "ManageProperties" });
        })
        .catch((response) => {
          console.error("Couldn't add property", response);
          this.showError = true;
          this.errorMessage = "Couldn't add property please try again";
        });
    },
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.token;
    },
    isAdmin() {
      return this.$store.state.user && this.$store.state.user.role === "admin";
    },
  },
};
</script>

<style>
.addproperty {
  background-image: linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2));
}

</style>