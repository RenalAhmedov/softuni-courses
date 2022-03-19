import { editItem, getById } from '../api/data.js';
import { html, until } from '../lib.js';

const editTemplate = (itemPromise) => html`
<div class="row space-top">
    <div class="col-md-12">
        <h1>Edit Furniture</h1>
        <p>Please fill all fields.</p>
    </div>
    ${until(itemPromise, html`<p>Loading &hellip;</p>`)}
</div>`;

const formTeplate = (item, onSubmit, errorMsg, errors) => html`
<form @submit=${onSubmit}>
    ${errorMsg ? html`<div class="form-group error">${errorMsg}</div>` : null} 
    <div class="row space-top">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-make">Make</label>
                <input class=${'form-control' + (errors.make ? ' is-invalid'  : ' is-valid' )} id="new-make" type="text" name="make" .value=${item.make}>
            </div>
            <div class="form-group has-success">
                <label class="form-control-label" for="new-model">Model</label>
                <input class=${'form-control' + (errors.model ? ' is-invalid'  : ' is-valid' )} id="new-model" type="text" name="model" .value=${item.model}>
            </div>
            <div class="form-group has-danger">
                <label class="form-control-label" for="new-year">Year</label>
                <input class=${'form-control' + (errors.year ? ' is-invalid'  : ' is-valid' )} id="new-year" type="number" name="year" .value=${item.year}>
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-description">Description</label>
                <input class=${'form-control' + (errors.description ? ' is-invalid'  : ' is-valid' )} id="new-description" type="text" name="description"
                    .value=${item.description}>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-price">Price</label>
                <input class=${'form-control' + (errors.price ? ' is-invalid'  : ' is-valid' )} id="new-price" type="number" name="price" .value=${item.price}>
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-image">Image</label>
                <input class=${'form-control' + (errors.img ? ' is-invalid'  : ' is-valid' )} id="new-image" type="text" name="img" .value=${item.img}>
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-material">Material (optional)</label>
                <input class="form-control is-valid" id="new-material" type="text" name="material" .value=${item.material}>
            </div>
            <input type="submit" class="btn btn-info" value="Edit" />
        </div>
    </div>
</form>`;

export function editPage(ctx) {
    const itemPromise = getById(ctx.params.id);

    update(itemPromise, null, {});

    function update(itemPromise, errorMsg, errors) {
        ctx.render(editTemplate(loadItem(itemPromise, errorMsg, errors)));
    }

    async function loadItem(itemPromise, errorMsg, errors) {
        const item = await itemPromise;
        return formTeplate(item, onSubmit, errorMsg, errors);
    }

    async function onSubmit(event) {
        event.preventDefault();
        const formData = [...(new FormData(event.target))];
        const data = formData.reduce((a, [k, v]) => Object.assign(a, { [k]: v.trim() }), {});

        const missing = formData.filter(([k, v]) => k != 'material' && v.trim() == '');
        try {
            if (missing.length > 0) {
                const errors = missing.reduce((a, [k]) => Object.assign(a, { [k]: true }), {});
                throw {
                    error: new Error('Please fill all required fields!'),
                    errors
                }
            }

            data.year = Number(data.year);
            data.price = Number(data.price);

            if (data.make.length < 4 || data.model.length < 4) {
                throw {
                    error: new Error('Make and Model must be at least 4 symbols long!'),
                    errors: { make: true, model: true }
                }
            }
            if (data.year < 1950 || data.year > 2050) {
                throw {
                    error: new Error('Year must be between 1950 and 2050!'),
                    errors: { year: true }
                }
            }
            if (data.description.length < 11) {
                throw {
                    error: new Error('Description must be more than 10 symbols!'),
                    errors: { description: true }
                }
            }
            if (data.price < 0) {
                throw {
                    error: new Error('Price must be a positive number!'),
                    errors: { price: true }
                }
            }

            const result = await editItem(ctx.params.id, data);
            event.target.reset();
            ctx.page.redirect('/details/' + result._id);
        } catch (err) {
            const message = err.message || err.error.message;
            update(data, message, err.errors || {});
        }
    }
}