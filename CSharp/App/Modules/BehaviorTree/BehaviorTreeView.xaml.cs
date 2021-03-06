﻿using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Infrastructure;

namespace Modules.BehaviorTree
{
	/// <summary>
	/// BehaviorTreeView.xaml 的交互逻辑
	/// </summary>
	[ViewExport(RegionName = "TreeCanvasRegion"), PartCreationPolicy(CreationPolicy.NonShared)]
	public partial class BehaviorTreeView
	{
		private const double DragThreshold = 5;
		private bool isDragging;
		private bool isLeftButtonDown;
		private Point origMouseDownPoint;

		public BehaviorTreeView()
		{
			this.InitializeComponent();
		}

		[Import]
		private BehaviorTreeViewModel ViewModel
		{
			get
			{
				return this.DataContext as BehaviorTreeViewModel;
			}
			set
			{
				this.DataContext = value;
			}
		}

		private void MenuNewNode_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Point point = Mouse.GetPosition(this.listBox);
			var treeNode = new TreeNode(point.X, point.Y);

			// one root node
			if (this.ViewModel.TreeNodes.Count == 0)
			{
				this.ViewModel.Add(treeNode, null);
			}
			else
			{
				if (this.listBox.SelectedItem != null)
				{
					var treeNodeViewModel = this.listBox.SelectedItem as TreeNodeViewModel;
					this.ViewModel.Add(treeNode, treeNodeViewModel);
				}
			}
			this.listBox.SelectedItem = null;
			e.Handled = true;
		}

		private void MenuDeleteNode_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (this.listBox.SelectedItem == null)
			{
				return;
			}
			var treeNodeViewModel = this.listBox.SelectedItem as TreeNodeViewModel;
			this.ViewModel.Remove(treeNodeViewModel);
			this.listBox.SelectedItem = null;
			e.Handled = true;
		}

		private void ListBoxItem_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton != MouseButton.Left)
			{
				return;
			}
			this.isLeftButtonDown = true;

			var item = (FrameworkElement) sender;
			var treeNodeViewModel = item.DataContext as TreeNodeViewModel;

			this.listBox.SelectedItem = treeNodeViewModel;

			this.origMouseDownPoint = e.GetPosition(this);

			item.CaptureMouse();
			e.Handled = true;
		}

		private void ListBoxItem_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (!this.isLeftButtonDown)
			{
				this.isDragging = false;
				return;
			}

			var item = (FrameworkElement) sender;
			var treeNodeViewModel = item.DataContext as TreeNodeViewModel;

			if (!this.isDragging)
			{
				this.listBox.SelectedItem = treeNodeViewModel;
			}

			this.isLeftButtonDown = false;
			this.isDragging = false;

			item.ReleaseMouseCapture();
			e.Handled = true;
		}

		private void ListBoxItem_MouseMove(object sender, MouseEventArgs e)
		{
			var item = (FrameworkElement) sender;
			var treeNodeViewModel = item.DataContext as TreeNodeViewModel;
			if (treeNodeViewModel == null)
			{
				return;
			}
			if (!treeNodeViewModel.IsRoot)
			{
				return;
			}

			Point curMouseDownPoint;
			Vector dragDelta;
			if (this.isDragging)
			{
				curMouseDownPoint = e.GetPosition(this);
				dragDelta = curMouseDownPoint - this.origMouseDownPoint;

				this.origMouseDownPoint = curMouseDownPoint;

				this.ViewModel.Move(dragDelta.X, dragDelta.Y);
				return;
			}

			if (!this.isLeftButtonDown)
			{
				return;
			}

			curMouseDownPoint = e.GetPosition(this);
			dragDelta = curMouseDownPoint - this.origMouseDownPoint;
			double dragDistance = Math.Abs(dragDelta.Length);
			if (dragDistance > DragThreshold)
			{
				this.isDragging = true;
			}
			e.Handled = true;
		}
	}
}