import {
  Component,
  EventEmitter,
  input,
  Input,
  OnInit,
  Output,
} from '@angular/core';

declare var bootstrap: any;

@Component({
  selector: 'app-url-delete',
  templateUrl: './url-delete.component.html',
  styleUrls: ['./url-delete.component.css'],
  standalone: false,
})
export class UrlDeleteComponent implements OnInit {
  @Input() selectedAlias: string = '';
  @Input() urlIdToDelete: number | null = null;
  @Output() confirmDelete = new EventEmitter<number>();
  inputAlias: string = '';

  confirmDeletion(): void {
    if (this.urlIdToDelete !== null) {
      this.confirmDelete.emit(this.urlIdToDelete);
      this.closeModal();
    }
  }

  ngOnInit(): void {
    console.log(this.urlIdToDelete)
    
    const modalElement = document.getElementById('confirmDeleteModal');
    if (modalElement) {
      modalElement.addEventListener('hidden.bs.modal', () => {
        this.inputAlias = '';
      });
    }
  }

  closeModal(): void {
    const modalElement = document.getElementById('confirmDeleteModal');
    if (modalElement) {
      const modalInstance = bootstrap.Modal.getInstance(modalElement);
      modalInstance?.hide();
    }
  }
}
