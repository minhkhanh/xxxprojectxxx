﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DTO;
using BUS;

namespace Presentation
{
    public partial class frThemChiTietPhieuXuat : Form
    {
        public ChiTietPhieuXuatDTO chiTietPhieuXuat;
        public frThemChiTietPhieuXuat()
        {
            InitializeComponent();
        }

        private void frThemChiTietPhieuXuat_Load(object sender, EventArgs e)
        {
            List<MatHangDTO> dsMatHang = MatHangBUS.LayDanhSachMatHang();
            List<DonViTinhDTO> dsDonViTinh = DonViTinhBUS.LayDanhSachDonViTinh();
            cbMatHang.DataSource = dsMatHang;
            cbMatHang.DisplayMember = "TenMatHang";
            cbDonViTinh.DataSource = dsDonViTinh;
            cbDonViTinh.DisplayMember = "TenDonViTinh";
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            chiTietPhieuXuat = new ChiTietPhieuXuatDTO();
            Close();
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            if (cbDonViTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Phải chọn đơn vị tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbMatHang.SelectedIndex < 0)
            {
                MessageBox.Show("Phải chọn tên mặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtDonGia.Text = txtDonGia.Text.Trim();
            txtSoLuong.Text = txtSoLuong.Text.Trim();
            txtThanhTien.Text = txtThanhTien.Text.Trim();
            try
            {
                if (Int32.Parse(txtDonGia.Text) <= 0)
                {
                    MessageBox.Show("Phải nhập đơn giá lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Int32.Parse(txtSoLuong.Text) <= 0)
                {
                    MessageBox.Show("Phải nhập đơn giá lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Int32.Parse(txtThanhTien.Text) <= 0)
                {
                    MessageBox.Show("Phải nhập đơn giá lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Phải nhập đơn giá số lượng thành tiền là những số lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                chiTietPhieuXuat = new ChiTietPhieuXuatDTO();
                chiTietPhieuXuat.MaMatHang = ((MatHangDTO)cbMatHang.Items[cbMatHang.SelectedIndex]).MaMatHang;
                chiTietPhieuXuat.MaDonViTinh = ((DonViTinhDTO)cbDonViTinh.Items[cbDonViTinh.SelectedIndex]).MaDonViTinh;
                chiTietPhieuXuat.DonGia = Int32.Parse(txtDonGia.Text);
                chiTietPhieuXuat.SoLuongXuat = Int32.Parse(txtSoLuong.Text);
                chiTietPhieuXuat.ThanhTien = Int32.Parse(txtThanhTien.Text);
                chiTietPhieuXuat.MaChiTietPhieuXuat = 1;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Có lỗi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSoLuong_Leave(object sender, EventArgs e)
        {
            txtDonGia.Text = txtDonGia.Text.Trim();
            txtSoLuong.Text = txtSoLuong.Text.Trim();
            int iDonGia;
            int iSoLuong;
            try
            {
                iDonGia = Int32.Parse(txtDonGia.Text);
                iSoLuong = Int32.Parse(txtSoLuong.Text);
            }
            catch (System.Exception ex)
            {
                return;
            }
            txtThanhTien.Text = (iSoLuong * iDonGia).ToString();
        }

        private void txtDonGia_Leave(object sender, EventArgs e)
        {
            txtSoLuong_Leave(sender, e);
        }
    }
}
