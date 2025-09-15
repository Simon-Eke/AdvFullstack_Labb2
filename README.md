# AdvFullstack_Labb2

## Description:

- Centralized ApiRoutes for url
- Generic IApiClient for basic api calls
- http cookie for JWT
- 

## Notes:

- Add areas for admin routes
- make [admin]controller : from basecontroller
- flesh out accountcontroller
- Login/Logout button
- Apply filters for error handling and other cool filters
- FluentValidation
- Pagination and search/order
- Fallback for missing route like a 404 page or something.

## Todos:

- Finish the menuitems controller with vm:s and views - DONE
- Copy the design to the other admin controllers
	1. Booking needs fixing in api. - DONE
		a) Get the list of bookings and then sort by Date.
		b) Create and update have similar flow. 
	2. Table should be quite easy
	3. Customers isnt required
	4. Admins is needed since I dont have a register
- Public routes: 
	1. Do HomeController with description and top 3 menuItems - DONE

	2. Do menuController - straight view - DONE
- Layout.cshtml:

	1. Ensure navigation to Booking (the public route) 

- JWT - DONE
- Change UI to be at least readable - done i guess....
- Pacman stuff is not responsive for smaller screens..........
- Move the the public reused MenuItemCard into a partial view