import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

import { IStory } from '@app/models/story.model';
import { HackerNewsService } from '@app/services/hacker-news.service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-story-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatTableModule,
    MatPaginatorModule
  ],
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit, OnDestroy {
  public stories: IStory[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  searchForm!: FormGroup;
  public searchValue = new FormControl();
  public displayedColumns: string[] = ['id', 'title', 'url'];
  public dataSource = new MatTableDataSource<IStory>();
  public subs = new Subscription();

  constructor(private hackerNewsService: HackerNewsService, private fb: FormBuilder) { }

  // #region Lifecycle methods.

  ngOnInit(): void {
    this.initForm();
    this.fetchStories();
    this.setupFilter();
  }

  ngOnDestroy(): void {
    if (this.subs) {
      this.subs.unsubscribe();
    }
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  // #endregion Lifecycle methods.

  public resetSearchValue(): void {
    this.searchForm.get('searchValue')?.patchValue('');
  }

  public fetchStories(): void {
    this.subs.add(this.hackerNewsService.getNewestStories().subscribe((stories: IStory[]) => {
      this.stories = stories;
      this.dataSource = new MatTableDataSource<IStory>(this.stories);
      this.dataSource.paginator = this.paginator;
    }));
  }

  private setupFilter(): void {
    this.dataSource.filterPredicate = (record, filter) => {
      if (!filter) {
        return true;
      }

      if (record.title.toLowerCase().indexOf(filter) > -1 || (record.url && record.url.toLowerCase().indexOf(filter) > -1)) {
        return true;
      }

      return false;
    }
  }

  private initForm(): void {
    this.searchForm = this.fb.group({
      searchValue: [''],
    });

    this.subs.add(this.searchForm.get('searchValue')?.valueChanges.subscribe(
      {
        next: (v: string) => {
          this.dataSource.filter = v.toLocaleLowerCase();
        }
      }
    ));
  }
}
