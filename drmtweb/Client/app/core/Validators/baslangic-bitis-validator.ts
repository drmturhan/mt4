import { ValidatorFn, AbstractControl } from '@angular/forms';
export class MTValidators {
    public static BitisBaslangictanOnceOlamaz(baslangicAdi: string = '', bitisAdi: string = ''): ValidatorFn {
        return (c: AbstractControl): { [key: string]: boolean } | null => {
            if (!baslangicAdi) baslangicAdi = 'baslangic';
            if (!bitisAdi) bitisAdi = 'bitis';
            let startControl = c.get(baslangicAdi);
            let endControl = c.get(bitisAdi);
            if (startControl != null && endControl != null) {
                if (startControl.pristine && endControl.pristine) {
                    return null;
                }
                if (startControl.value === '' || endControl.value === '') return null;

                if (startControl.value < endControl.value) {
                    return null;
                }
            }
            return { 'bitisbaslangictanonceolamaz': true };
        }

    }
}