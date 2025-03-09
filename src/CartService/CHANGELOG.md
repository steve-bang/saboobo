# Change log

## v1.1.2

### Fixed
- Fix error not load product image in payload request in api add items to cart.



## v1.1.0

### Added
- Add productImage field in CartItem.


## v1.0.0

### Added
- Add api create cart `POST api/v1/carts/{id}`
- Add api add items to cart  `POST api/v1/carts/{id}/items`
- Add api update items to cart `PUT api/v1/carts/{id}/items`
- Add api remove items from cart `DELETE api/v1/carts/{id}/items`
- Add api place order `POST api/v1/carts/{id}/place-order`